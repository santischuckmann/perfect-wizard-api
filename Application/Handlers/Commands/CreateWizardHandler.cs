using AutoMapper;
using Azure.Core;
using MediatR;
using MongoDB.Driver;
using perfect_wizard.Application.Commands;
using perfect_wizard.Application.DTOs;
using perfect_wizard.Models;

namespace perfect_wizard.Application.Handlers.Commands
{
    public class CreateWizardHandler : IRequestHandler<CreateWizardCommand, string>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        private readonly IMapper _mapper;
        public CreateWizardHandler(Infrastructure.MongoDBService mongoDBService, IMapper mapper)
        {
            _dbService = mongoDBService;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateWizardCommand request, CancellationToken cancellationToken)
        {
            ValidateFields(request.Wizard.Screens);

            string wizardId = Guid.NewGuid().ToString();
            request.Wizard.WizardId = wizardId;

            Wizard wizard = _mapper.Map<Wizard>(request.Wizard);
            PrepareFields(wizard);

            await _dbService.Wizard.InsertOneAsync(wizard, new InsertOneOptions { }, cancellationToken);

            return wizardId;
        }

        private static void PrepareFields(Wizard wizard)
        {
            foreach (var screen in wizard.screens)
                foreach (var field in screen.fields)
                    field.FieldId = Guid.NewGuid().ToString();
        }

        private static void ValidateFields(List<ScreenDto> screens)
        {
            List<DTOs.FieldDto> allFields = screens.SelectMany(s => s.Fields).ToList();

            if (!allFields.Where(x => x.IsIdentifier).Any())
                throw new Exception("At least one identifier field is required");

            foreach (var field in allFields)
            {
                if (field.MinValuesRequired == 0 && field.IsIdentifier)
                    throw new Exception("An identifier field must be required");

                if ((field.Type == FieldType.Options || field.Type == FieldType.Multiple || field.Type == FieldType.Radio) &&
                    field.Options.Count == 0)
                    throw new Exception("Field of type OPTIONS or MULTIPLE must have at least one option");

            }
        }
    }
}
