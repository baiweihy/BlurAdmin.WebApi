﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using LegacyApplication.Shared.ByModule.HumanResources.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.HumanResources
{
    public class Employee : EntityBase
    {
        public string Name { get; set; }
        public string No { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public int? DepartmentId { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public PoliticalStatus PoliticalStatus { get; set; }
        public string EmailAddress { get; set; }
        public string Remark { get; set; }

        public EmployeeNature EmployeeNature { get; set; }
        public EducationNature EducationNature { get; set; }
        public EducationDegree EducationDegree { get; set; }
        public EducationalBackground EducationalBackground { get; set; }

        public int? JobPostId { get; set; }
        public int? NationalityId { get; set; }
        public string Title { get; set; }
        public int? TitleLevelId { get; set; }
        public int? AdministrativeLevelId { get; set; }

        public Department Department { get; set; }
        public JobPost JobPost { get; set; }
        public Nationality Nationality { get; set; }
        public TitleLevel TitleLevel { get; set; }
        public AdministrativeLevel AdministrativeLevel { get; set; }
    }

    public class EmployeeConfiguraton : EntityBaseConfiguration<Employee>
    {
        public EmployeeConfiguraton()
        {
            ToTable("hr.Employee");

            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.No).IsRequired().HasMaxLength(50).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));
            Property(x => x.IdNumber).HasMaxLength(50).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            Property(x => x.PhoneNumber).HasMaxLength(50);
            Property(x => x.EmailAddress).HasMaxLength(50);
            Property(x => x.Remark).HasMaxLength(500);
            Property(x => x.Title).HasMaxLength(100);

            HasOptional(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => x.DepartmentId);
            HasOptional(x => x.JobPost).WithMany().HasForeignKey(x => x.JobPostId);
            HasOptional(x => x.Nationality).WithMany().HasForeignKey(x => x.NationalityId);
            HasOptional(x => x.TitleLevel).WithMany().HasForeignKey(x => x.TitleLevelId);
            HasOptional(x => x.AdministrativeLevel).WithMany().HasForeignKey(x => x.AdministrativeLevelId);
        }
    }
}
