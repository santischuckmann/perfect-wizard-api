using AutoMapper;

namespace perfect_wizard.Infrastructure.Mapper
{
    public class WizardProfile: Profile
    {
        public WizardProfile()
        {
            CreateMap<Application.DTOs.WizardDto, Models.Wizard>().ReverseMap();
            CreateMap<Application.DTOs.ScreenDto, Models.Screen>().ReverseMap();
            CreateMap<Application.DTOs.FieldDto, Models.Field>().ReverseMap();

            CreateMap<Application.DTOs.ResponseDto, Models.Response>().ReverseMap();
            CreateMap<Application.DTOs.ResponseFieldDto, Models.ResponseField>().ReverseMap();

            CreateMap<Application.DTOs.UserDto, Models.User>().ReverseMap();

            CreateMap<Application.DTOs.TenantDto, Models.Tenant>().ReverseMap();
        }
    }
}
