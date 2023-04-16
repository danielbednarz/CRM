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
            CreateMap<AddClientNoteVM, ClientNote>();
            CreateMap<AddClientDocumentVM, ClientDocument>();
            CreateMap<AddUserTaskVM, UserTask>();
            CreateMap<AddUserTaskCommentVM, UserTaskComment>();
            CreateMap<EditUserVM, AppUser>();

            CreateMap<UserTask, UserTaskDTO>().ForMember(dest => dest.Comments, act => act.Ignore()).ForMember(dest => dest.History, act => act.Ignore());
            CreateMap<UserTaskComment, UserTaskCommentDTO>();

        }
    }
}
