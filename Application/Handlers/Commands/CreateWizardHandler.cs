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
            string wizardId = Guid.NewGuid().ToString();

            Wizard wizard = new()
            {
               tenantId = request.TenantId,
               WizardId = wizardId
            };
            FillInitialFields(wizard);

            await _dbService.Wizard.InsertOneAsync(wizard, new InsertOneOptions { }, cancellationToken);

            return wizardId;
        }

        private void FillInitialFields(Wizard wizard)
        {
            wizard.screens.Add(new Screen
            {
                stepName = "My first step",
            });

            wizard.screens[0].fields.Add(new Field
            {
                name = "name",
                description = new Description() { text = "Description (optional)", position = "above" },
                FieldId = Guid.NewGuid().ToString(),
                isIdentifier = true,
                label = "What is your name?",
                minValuesRequired = 1,
                options = new(),
                order = 1,
                placeholder = "John Doe",
                type = FieldType.Text
            });
        }
    }
}
