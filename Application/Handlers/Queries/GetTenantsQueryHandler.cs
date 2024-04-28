using AutoMapper;
using MediatR;
using MongoDB.Driver;
using perfect_wizard.Application.DTOs;
using perfect_wizard.Application.Queries;
using perfect_wizard.Models;

namespace perfect_wizard.Application.Handlers.Queries
{
    public class GetTenantsQueryHandler : IRequestHandler<GetTenantsQuery, List<DTOs.TenantDto>>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        private readonly IMapper _mapper;
        public GetTenantsQueryHandler(Infrastructure.MongoDBService mongoDBService, IMapper mapper)
        {
            _dbService = mongoDBService;
            _mapper = mapper;
        }
        public async Task<WizardDto> Handle(GetWizardQuery request, CancellationToken cancellationToken)
        {
            Wizard wizard = await _dbService.Wizard.Find(x => x.WizardId == request.WizardId).FirstOrDefaultAsync(cancellationToken);

            if (wizard is null)
                throw new Exception($"There is no Wizard that matches the id = '{request.WizardId}'");

            WizardDto wizardDto = _mapper.Map<Wizard, WizardDto>(wizard);

            return wizardDto;
        }

        public async Task<List<TenantDto>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
        {
            List<Models.Tenant> tenants = await _dbService.Tenant.Find(x => x.UserId == request.UserId).ToListAsync(cancellationToken);

            var tenantDtos = _mapper.Map<List<TenantDto>>(tenants);

            return tenantDtos;
        }
    }
}
