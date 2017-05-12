using System.Collections.Generic;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Scrum
{
    public class Project : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectManager { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<Sprint> Sprints { get; set; }
        public virtual ICollection<ProjectTeamMember> ProjectTeamMemberss { get; set; }
    }

    public class ProjectConfiguration : EntityBaseConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            ToTable("Scrum.Project");

            Property(x => x.Name).HasMaxLength(100).IsRequired();
            Property(x => x.Description).HasMaxLength(4000);
            Property(x => x.ProjectManager).HasMaxLength(100).IsRequired();
        }
    }
}
