using AutoMapper;
using MediatR;
using MongoDB.Driver;
using perfect_wizard.Application.Commands;
using perfect_wizard.Models;
using ServiceStack;

namespace perfect_wizard.Application.Handlers.Commands
{
    public class SendWizardHandler : IRequestHandler<SendWizardCommand>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        private readonly IMapper _mapper;
        public SendWizardHandler(Infrastructure.MongoDBService mongoDBService, IMapper mapper)
        {
            _dbService = mongoDBService;
            _mapper = mapper;
        }
        public async Task Handle(SendWizardCommand request, CancellationToken cancellationToken)
        {
            var wizard = await _dbService.Wizard.Find(x => x.WizardId == request.Response.WizardId).FirstOrDefaultAsync(cancellationToken);

            if (wizard is null)
                throw new Exception($"No matching Wizard with WizardId = {request.Response.WizardId}");

            bool alreadyResponded = await ResponseWithMatchingIdentifiersExist(request.Response, wizard, cancellationToken);

            if (alreadyResponded)
                throw new Exception("You have already participated in this wizard.");

            List<Models.Field> allFields = wizard.screens.SelectMany(s => s.fields).ToList();

            foreach (var field in allFields)
            {
                var responseToField = request.Response.ResponseFields.Find(rf => rf.FieldId.Equals(field));

                if (responseToField is null)
                    throw new Exception($"No response was given for field with name {field.name}");

                ValidateResponse(field, responseToField);
            }

            var response = _mapper.Map<Response>(request.Response);

            await _dbService.Response.InsertOneAsync(response, new InsertOneOptions { }, cancellationToken);
        }

        private async Task<bool> ResponseWithMatchingIdentifiersExist(DTOs.ResponseDto response, Models.Wizard wizard, CancellationToken cancellationToken)
        {
            List<Models.Field> identifierFields = wizard.screens
                .SelectMany(s => s.fields)
                .Where(f => f.isIdentifier)
                .ToList();

            var allResponseFields = await _dbService.Response
                .Find(x => x.WizardId == wizard.WizardId)
                .ToListAsync(cancellationToken);

            var allIdentifierResponseFields = allResponseFields
                .Map(x => x.responseFields)
                .Where(f => identifierFields.Where(f => f.FieldId.Equals(f.FieldId)).Any())
                .Map(f => TurnIdentifiersIntoOne(f.Map(x => x.values[0])))
                .ToList();

            var identifierResponseFields = response.ResponseFields
                .Where(f => identifierFields.Where(f => f.FieldId.Equals(f.FieldId)).Any())
                .ToList();

            string currentResponseIdentifier = TurnIdentifiersIntoOne(identifierResponseFields.Map(x => x.Values[0]));

            return allIdentifierResponseFields.Contains(currentResponseIdentifier);
        }

        private static string TurnIdentifiersIntoOne(List<string> identifiers) => string.Join('-', identifiers);

        private static void ValidateResponse(Models.Field field, DTOs.ResponseField responseToField)
        {
            if (field.minValuesRequired == 1 && responseToField.Values.Length == 0)
                throw new Exception($"Required field {field.name} was not completed");

            if ((field.type == FieldType.Options || field.type == FieldType.Multiple) &&
                field.options.Where(x => responseToField.Values.Contains(x.id)).Any())
                throw new Exception($"Value in field: {field.name} is not an option");

            if (field.type == FieldType.Multiple && field.minValuesRequired != responseToField.Values.Length)
                throw new Exception($"Field: {field.name} should be sent {field.minValuesRequired} values");
        }
    }
}
