using AdminProjectsDemo.DTOs.Activities.Request;
using AdminProjectsDemo.DTOs.Projects.Request;
using AdminProjectsDemo.Entitites;
using AutoMapper;

namespace AdminProjectsDemo.Mapper
{
    public class AdminProjectsProfile : Profile
    {
        public AdminProjectsProfile()
        {
            CreateMap<ProjectCreationRequest, Proyecto>();
            CreateMap<ProjectUpdateRequest, Proyecto>();

            CreateMap<ActivityCreationRequest, Actividad>();
            CreateMap<ActivityUpdateRequest, Actividad>();
        }
    }
}
