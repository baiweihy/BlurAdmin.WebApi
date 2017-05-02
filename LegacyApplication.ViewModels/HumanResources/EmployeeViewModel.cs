using System;
using System.ComponentModel.DataAnnotations;
using LegacyApplication.Shared.ByModule.HumanResources.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.HumanResources
{
    public class EmployeeViewModel : EntityBase
    {
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(100, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Name { get; set; }

        [Display(Name = "员工编号")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}的长度不可超过{1}")]
        public string No { get; set; }
        
        [Display(Name = "状态")]
        public EmployeeStatus EmployeeStatus { get; set; }
        [Display(Name = "状态")]
        public string EmployeeStatusDisplay => EmployeeStatus.ToString();

        public int? DepartmentId { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        [Display(Name = "性别")]
        public string GenderDisplay => Gender.ToString();

        [Display(Name = "出生日期")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "身份证号")]
        [StringLength(20, ErrorMessage = "{0}的长度不可超过{1}")]
        public string IdNumber { get; set; }

        [Display(Name = "电话号码")]
        [StringLength(50, ErrorMessage = "{0}的长度不可超过{1}")]
        public string PhoneNumber { get; set; }

        [Display(Name = "婚姻状况")]
        public MaritalStatus MaritalStatus { get; set; }
        [Display(Name = "婚姻状况")]
        public string MaritalStatusDisplay => MaritalStatus.ToString();

        [Display(Name = "政治面貌")]
        public PoliticalStatus PoliticalStatus { get; set; }
        [Display(Name = "政治面貌")]
        public string PoliticalStatusDisplay => PoliticalStatus.ToString();

        [Display(Name = "电子邮件")]
        [StringLength(50, ErrorMessage = "{0}的长度不可超过{1}")]
        public string EmailAddress { get; set; }

        [Display(Name = "备注")]
        [StringLength(500, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Remark { get; set; }

        [Display(Name = "人员性质")]
        public EmployeeNature EmployeeNature { get; set; }
        [Display(Name = "人员性质")]
        public string EmployeeNatureDisplay => EmployeeNature.ToString();

        [Display(Name = "学历性质")]
        public EducationNature EducationNature { get; set; }
        [Display(Name = "学历性质")]
        public string EducationNatureDisplay => EducationNature.ToString();

        [Display(Name = "学位")]
        public EducationDegree EducationDegree { get; set; }
        [Display(Name = "学位")]
        public string EducationDegreeDisplay => EducationDegree.ToString();

        [Display(Name = "岗位ID")]
        public int? JobPostId { get; set; }

        public JobPostViewModel Post { get; set; }
        public DepartmentViewModel Department { get; set; }
    }
}
