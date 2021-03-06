﻿using AutoMapper;
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
            CreateMap<TodoViewModel, Todo>();
            CreateMap<ScheduleViewModel, Schedule>();

            CreateMap<DepartmentViewModel, Department>()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<JobPostLevelViewModel, JobPostLevel>();
            CreateMap<JobPostViewModel, JobPost>();
            CreateMap<AdministrativeLevelViewModel, AdministrativeLevel>();
            CreateMap<TitleLevelViewModel, TitleLevel>();
            CreateMap<NationalityViewModel, Nationality>();
            
        }
    }
}