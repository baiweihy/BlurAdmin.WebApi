using System.Linq;
using AutoMapper;
using LegacyApplication.Models.Core;
using LegacyApplication.Models.HumanResources;
using LegacyApplication.Models.Work;
using LegacyApplication.ViewModels.Core;
using LegacyApplication.ViewModels.HumanResources;
using LegacyStandalone.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using LegacyApplication.ViewModels.Work;

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
            CreateMap<InternalMail, SentMailViewModel>()
                .ForMember(dest => dest.AttachmentCount, opt => opt.MapFrom(ori => ori.Attachments.Count))
                .ForMember(dest => dest.HasAttachments, opt => opt.MapFrom(ori => ori.Attachments.Any()))
                .ForMember(dest => dest.ToCount, opt => opt.MapFrom(ori => ori.Tos.Count))
                .ForMember(dest => dest.AnyoneRead, opt => opt.MapFrom(ori => ori.Tos.Any(y => y.HasRead)))
                .ForMember(dest => dest.AllRead, opt => opt.MapFrom(ori => ori.Tos.All(y => y.HasRead)));
            CreateMap<Todo, TodoViewModel>();
            CreateMap<Schedule, ScheduleViewModel>();

            CreateMap<Department, DepartmentViewModel>()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<JobPostLevel, JobPostLevelViewModel>();
            CreateMap<JobPost, JobPostViewModel>();
            CreateMap<AdministrativeLevel, AdministrativeLevelViewModel>();
            CreateMap<TitleLevel, TitleLevelViewModel>();
            CreateMap<Nationality, NationalityViewModel>();
            
        }
    }
}