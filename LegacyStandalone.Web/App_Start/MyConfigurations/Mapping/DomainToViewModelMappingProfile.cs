﻿using AutoMapper;
using LegacyApplication.Models.Administration;
using LegacyApplication.Models.Core;
using LegacyApplication.ViewModels.Administration;
using LegacyApplication.ViewModels.Core;
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
        }
    }
}