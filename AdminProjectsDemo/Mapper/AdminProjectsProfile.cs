using AdminProjectsDemo.DTOs.Activities.Request;
using AdminProjectsDemo.DTOs.Beneficiaries.Request;
using AdminProjectsDemo.DTOs.Executors.Request;
using AdminProjectsDemo.DTOs.Projects.Request;
using AdminProjectsDemo.DTOs.ProjectsBeneficiaries.Request;
using AdminProjectsDemo.DTOs.ProjectsExecutors.Request;
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
            CreateMap<BeneficiaryCreationRequest, Beneficiario>();
            CreateMap<BeneficiaryUpdateRequest, Beneficiario>();
            CreateMap<ExecutorCreationRequest, Ejecutor>();
            CreateMap<ExecutorUpdateRequest, Ejecutor>();
            CreateMap<ProjectBeneficiaryCreationRequest, ProyectoBeneficiario>();
            CreateMap<ProjectExecutorCreationRequest, ProyectoEjecutor>();
        }
    }
}
