using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterVM, AppUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<AddClientVM, Client>();
            CreateMap<EditClientVM, Client>();
        }
    }
}
