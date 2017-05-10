using AutoMapper;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Models.Scrum;
using LegacyApplication.Models.Work;
using LegacyApplication.ViewModels.Core;
using LegacyApplication.ViewModels.HumanResources;
using LegacyApplication.ViewModels.Scrum;
using LegacyApplication.ViewModels.Work;
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

            CreateMap<InternalMail, InternalMailViewModel>();
            CreateMap<InternalMailTo, InternalMailToViewModel>();
            CreateMap<InternalMailAttachment, InternalMailAttachmentViewModel>();

            CreateMap<Department, DepartmentViewModel>()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<JobPostLevel, JobPostLevelViewModel>();
            CreateMap<JobPost, JobPostViewModel>();
            CreateMap<AdministrativeLevel, AdministrativeLevelViewModel>();
            CreateMap<AdministrativePost, AdministrativePostViewModel>();
            CreateMap<TitleLevel, TitleLevelViewModel>();
            CreateMap<TitlePost, TitlePostViewModel>();

            CreateMap<Project, ProjectViewModel>();
            CreateMap<Feature, FeatureViewModel>();
            CreateMap<Sprint, SprintViewModel>();
            CreateMap<ProductBacklogItem, ProductBacklogItemViewModel>();
            CreateMap<Bug, BugViewModel>();
            CreateMap<ProductBacklogItemTask, ProductBacklogItemTaskViewModel>();
            CreateMap<BugTask, BugTaskViewModel>();
            CreateMap<ProjectTeamMember, ProjectTeamMemberViewModel>();
        }
    }
}