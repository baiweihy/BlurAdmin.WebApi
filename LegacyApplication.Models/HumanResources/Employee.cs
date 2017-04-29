using System;
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
        public string UserName { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public int? DepartmentId { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public PoliticalStatus PoliticalStatus { get; set; }
        public string EmailAddress { get; set; }
        public string Remark { get; set; }

        public Department Department { get; set; }
    }

    public class EmployeeConfiguraton : EntityBaseConfiguration<Employee>
    {
        public EmployeeConfiguraton()
        {
            ToTable("hr.Employee");

            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.No).HasMaxLength(50).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            Property(x => x.UserName).HasMaxLength(50).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));
            Property(x => x.IdNumber).HasMaxLength(50).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            Property(x => x.PhoneNumber).HasMaxLength(50);
            Property(x => x.EmailAddress).HasMaxLength(50);
            Property(x => x.Remark).HasMaxLength(500);

            HasOptional(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => x.DepartmentId);
        }
    }
}
