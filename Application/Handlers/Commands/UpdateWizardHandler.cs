using AutoMapper;
using Azure.Core;
using MediatR;
using MongoDB.Driver;
using perfect_wizard.Application.Commands;
using perfect_wizard.Application.DTOs;
using perfect_wizard.Models;

namespace perfect_wizard.Application.Handlers.Commands
{
    public class UpdateWizardHandler : IRequestHandler<UpdateWizardCommand>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        private readonly IMapper _mapper;
        public UpdateWizardHandler(Infrastructure.MongoDBService mongoDBService, IMapper mapper)
        {
            _dbService = mongoDBService;
            _mapper = mapper;
        }
        public async Task Handle(UpdateWizardCommand request, CancellationToken cancellationToken)
        {
            ValidateWizard(request.Wizard);

            if (!await DoesWizardExist(request.Wizard.WizardId))
                throw new Exception("The wizard with given WizardId does not exist");

            Wizard wizard = _mapper.Map<Wizard>(request.Wizard);
            PrepareFields(wizard);

            await _dbService.Wizard.ReplaceOneAsync(
                Builders<Models.Wizard>.Filter.Eq(w => w.WizardId, request.Wizard.WizardId), 
                wizard,
                new ReplaceOptions() { },
                cancellationToken
            );
        }

        private async Task<bool> DoesWizardExist(string wizardId)
        {
            var wizard = await _dbService.Wizard
                .Find(x => x.WizardId == wizardId)
                .ToListAsync();

            return wizard.Any();
        }

        private static void PrepareFields(Wizard wizard)
        {
            foreach (var screen in wizard.screens)
                foreach (var field in screen.fields)
                {
                    if (string.IsNullOrEmpty(field.FieldId))
                        field.FieldId = Guid.NewGuid().ToString();
                }
        }

        private static void ValidateWizard(WizardDto wizard)
        {
            if (string.IsNullOrEmpty(wizard.WizardId))
                throw new Exception("WizardId must me especified");

            List<DTOs.FieldDto> allFields = wizard.Screens.SelectMany(s => s.Fields).ToList();

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
