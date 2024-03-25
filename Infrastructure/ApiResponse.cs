using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cedeira.Infraestructura.Entidades
{
    [Serializable]
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            this.meta = new Meta();
            this.data = default(T);
        }
        public Meta meta { get; set; }
        public T data { get; set; }
        public Error[] errors { get; set; }

        public void SetearExcepcion(Exception ex)
        {
            Error error = new Error();
            error.message = TextoCompletoExcepcion(ex);
            List<Error> errores = new List<Error>() { error };
            this.errors = errores.ToArray();
        }

        public static string TextoCompletoExcepcion(Exception Excepcion)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(Excepcion.Message);
            while (Excepcion.InnerException != null)
            {
                Excepcion = Excepcion.InnerException;
                sb.Append(" - ");
                sb.Append(Excepcion.Message);
            }
            return sb.ToString();
        }

    }

    public class ApiResponse
    {
        public ApiResponse()
        {
            this.Meta = new Meta();
        }
        public Meta Meta { get; set; }
        public string Data { get; set; }
        public Error[] Errors { get; set; }

        public void SetearExcepcion(Exception ex)
        {
            Error error = new Error();
            error.message = ex.Message;
            List<Error> errores = new List<Error>() { error };
            this.Errors = errores.ToArray();
        }
    }
    public class Meta
    {
        public string method { get; set; }
        public string operation { get; set; }
    }
    public class Error
    {
        public string code { get; set; }
        public int? status_code { get; set; }
        public string detail { get; set; }
        public string message { get; set; }
        public string lang { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string code_backend { get; set; }
        public string code_internal { get; set; }
        public string method_uri_path { get; set; }
        public string error_type { get; set; }
        public string data { get; set; }
        public string trace { get; set; }
    }
}
