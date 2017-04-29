using AutoMapper;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.ViewModels.Core;
using LegacyApplication.ViewModels.HumanResources;
using LegacyStandalone.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LegacyStandalone.Web.MyConfigurations.Mapping
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappings";

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, ApplicationUser>();
            CreateMap<RoleViewModel, IdentityRole>();
            CreateMap<RoleViewModel, IdentityUserRole>();

            CreateMap<UploadedFileViewModel, UploadedFile>();

            CreateMap<DepartmentViewModel, Department>()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            CreateMap<EmployeeViewModel, Employee>();
        }
    }
}