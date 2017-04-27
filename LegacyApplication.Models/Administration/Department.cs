using LegacyApplication.Shared.Configurations;
using LegacyApplication.Shared.Features.Tree;

namespace LegacyApplication.Models.Administration
{
    public class Department : TreeEntityBase<Department>
    {
        public string Name { get; set; }
    }

    public class DepartmentConfiguration : TreeEntityBaseConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}
