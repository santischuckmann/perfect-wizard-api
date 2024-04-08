using AutoMapper;

namespace perfect_wizard.Infrastructure.Mapper
{
    public class WizardProfile: Profile
    {
        public WizardProfile()
        {
            CreateMap<Application.DTOs.WizardDto, Models.Wizard>().ReverseMap();

            CreateMap<Application.DTOs.ResponseDto, Models.Response>().ReverseMap();
        }
    }
}
