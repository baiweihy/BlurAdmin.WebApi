using System.Collections.Generic;
using LegacyApplication.Shared.Features.Tree;

namespace LegacyApplication.Models.HumanResources
{
    public class Department : TreeEntityBase<Department>
    {
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }

    public class DepartmentConfiguration : TreeEntityBaseConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            ToTable("hr.Department");

            Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}
