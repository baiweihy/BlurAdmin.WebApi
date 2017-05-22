using AutoMapper;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Models.Scrum;
using LegacyApplication.Models.Work;
using LegacyApplication.ViewModels.Core;
using LegacyApplication.ViewModels.HumanResources;
using LegacyStandalone.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using LegacyApplication.ViewModels.Scrum;
using LegacyApplication.ViewModels.Work;

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

            CreateMap<InternalMailViewModel, InternalMail>();
            CreateMap<InternalMailToViewModel, InternalMailTo>();
            CreateMap<InternalMailAttachmentViewModel, InternalMailAttachment>();

            CreateMap<DepartmentViewModel, Department>()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<JobPostLevelViewModel, JobPostLevel>();
            CreateMap<JobPostViewModel, JobPost>();
            CreateMap<AdministrativeLevelViewModel, AdministrativeLevel>();
            CreateMap<TitleLevelViewModel, TitleLevel>();
            CreateMap<NationalityViewModel, Nationality>();
            CreateMap<AllowanceLevelViewModel, AllowanceLevel>();

            CreateMap<ProjectViewModel, Project>();
            CreateMap<FeatureViewModel, Feature>();
            CreateMap<SprintViewModel, Sprint>();
            CreateMap<ProductBacklogItemViewModel, ProductBacklogItem>();
            CreateMap<BugViewModel, Bug>();
            CreateMap<ProductBacklogItemTaskViewModel, ProductBacklogItemTask>();
            CreateMap<BugTaskViewModel, BugTask>();
            CreateMap<ProjectTeamMemberViewModel, ProjectTeamMember>();

        }
    }
}