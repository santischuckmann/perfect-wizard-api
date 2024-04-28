using AutoMapper;
using MediatR;
using perfect_wizard.Application.Commands;
using perfect_wizard.Models;

namespace perfect_wizard.Application.Handlers.Commands
{
    public class CreateTenantHandler : IRequestHandler<CreateTenantCommand, string>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        private readonly IMapper _mapper;
        public CreateTenantHandler(Infrastructure.MongoDBService mongoDBService, IMapper mapper)
        {
            _dbService = mongoDBService;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            string tenantId = Guid.NewGuid().ToString();

            Tenant tenant = _mapper.Map<Tenant>(request.Tenant);

            await _dbService.Tenant.InsertOneAsync(tenant, new MongoDB.Driver.InsertOneOptions { }, cancellationToken);

            return tenantId;
        }
    }
}
