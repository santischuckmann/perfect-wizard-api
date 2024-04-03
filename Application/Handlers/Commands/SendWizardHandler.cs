using AutoMapper;
using MediatR;
using MongoDB.Driver;
using perfect_wizard.Application.Commands;
using perfect_wizard.Models;

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
            var wizard = await _dbService.Wizard.Find(x => x.WizardId == request.ResponseDto.WizardId).FirstOrDefaultAsync(cancellationToken);

            if (wizard is null)
                throw new Exception($"No matching Wizard with WizardId = {request.ResponseDto.WizardId}");

            var response = _mapper.Map<Response>(request.ResponseDto);

            await _dbService.Response.InsertOneAsync(response, new InsertOneOptions { }, cancellationToken);
        }
    }
}
