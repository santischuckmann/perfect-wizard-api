using AutoMapper;
using MediatR;
using MongoDB.Driver;
using perfect_wizard.Application.Commands;
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
            request.Wizard.WizardId = wizardId;

            Wizard wizard = _mapper.Map<Wizard>(request.Wizard);

            await _dbService.Wizard.InsertOneAsync(wizard, new InsertOneOptions { }, cancellationToken);

            return wizardId;
        }
    }
}
