using AutoMapper;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Models.OnlineTraining;
using LegacyApplication.ViewModels.Core;
using LegacyApplication.ViewModels.HumanResources;
using LegacyApplication.ViewModels.OnlineTraining;
using LegacyStandalone.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LegacyStandalone.Web.MyConfigurations.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        public DomainToViewModelMappingProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<IdentityRole, RoleViewModel>();
            CreateMap<IdentityUserRole, RoleViewModel>();

            CreateMap<UploadedFile, UploadedFileViewModel>();

            CreateMap<Department, DepartmentViewModel>()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<JobPostLevel, JobPostLevelViewModel>();
            CreateMap<JobPost, JobPostViewModel>();
            CreateMap<Category, CategoryViewModel>();
        }
    }
}