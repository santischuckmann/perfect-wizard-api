using Cedeira.Infraestructura.Entidades;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using Newtonsoft.Json;
using ServiceStack;
using System.Runtime.CompilerServices;

namespace perfect_wizard.Infrastructure
{
    public class ResponseNormalizerMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseNormalizerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;

            using var newBody = new MemoryStream();
            context.Response.Body = newBody;

            List<Error> errors = new List<Error>();

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                errors = FlattenException(ex);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            finally
            {
                if (!string.IsNullOrEmpty(context.Response.Headers["endpoint_id"]))
                {
                    context.Response.ContentType = "application/json";

                    newBody.Seek(0, SeekOrigin.Begin);
                    var bodyResponse = await new StreamReader(newBody).ReadToEndAsync();

                    ApiResponse<object> response = new ApiResponse<object>()
                    {
                        errors = errors.ToArray(),
                        meta = new Meta()
                    };

                    if (bodyResponse.IsValidJson())
                    {
                        response.data = JsonConvert.DeserializeObject(bodyResponse);
                    }
                    else
                    {
                        response.data = bodyResponse;
                    }


                    newBody.Seek(0, SeekOrigin.Begin);
                    StreamWriter writer = new StreamWriter(newBody);
                    writer.Write(JsonConvert.SerializeObject(response));
                    writer.Flush();
                }

                newBody.Seek(0, SeekOrigin.Begin);
                await newBody.CopyToAsync(originalBody);
            }
        }

        private static List<Error> FlattenException(Exception ex)
        {
            List<Error> errors = new();
            do
            {
                errors.Add(new Error
                {
                    message = ex.Message,
                    trace = ex.StackTrace,
                    code = ex.ToErrorCode(),
                    status_code = ex.ToStatusCode(),
                }); ;

                ex = ex.InnerException;
            } while (ex != null);

            return errors;
        }
    }

    public static class Extensions
    {
        public static bool IsValidJson(this string json)
        {
            try
            {
                JsonConvert.DeserializeObject(json);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }
    }

    public class GlobalHeadersAttribute : ActionFilterAttribute
    {
        private readonly string _headerName;
        private readonly string _headerValue;

        public GlobalHeadersAttribute(string headerName, string headerValue)
        {
            _headerName = headerName;
            _headerValue = headerValue;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_headerName, _headerValue);
        }
    }
}