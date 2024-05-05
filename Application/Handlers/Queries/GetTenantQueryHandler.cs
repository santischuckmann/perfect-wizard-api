using AutoMapper;
using MediatR;
using MongoDB.Driver;
using perfect_wizard.Application.DTOs;
using perfect_wizard.Application.Queries;
using perfect_wizard.Models;

namespace perfect_wizard.Application.Handlers.Queries
{
    public class GetTenantQueryHandler : IRequestHandler<GetTenantQuery, DTOs.TenantDto>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        private readonly IMapper _mapper;
        public GetTenantQueryHandler(Infrastructure.MongoDBService mongoDBService, IMapper mapper)
        {
            _dbService = mongoDBService;
            _mapper = mapper;
        }
        public async Task<TenantDto> Handle(GetTenantQuery request, CancellationToken cancellationToken)
        {
            Models.Tenant tenant = await _dbService.Tenant.Find(x => x.TenantId == request.TenantId).FirstOrDefaultAsync(cancellationToken);

            var tenantDto = _mapper.Map<TenantDto>(tenant);

            return tenantDto;
        }
    }
}
