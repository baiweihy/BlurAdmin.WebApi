using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Scrum
{
    public class ProjectTeamMember: EntityBase
    {
        public int ProjectId { get; set; }

        public string UserName { get; set; }

        public virtual Project Project { get; set; }
    }

    public class ProjectTeamMemberConfiguration : EntityBaseConfiguration<ProjectTeamMember>
    {
        public ProjectTeamMemberConfiguration()
        {
            ToTable("Scrum.ProjectTeamMember");

            Property(x => x.UserName).HasMaxLength(100).IsRequired();

            Property(x => x.ProjectId).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Internal_ProjectTeamMembers_ProjectIdAndUserName", 0) { IsUnique = true }));
            Property(x => x.UserName).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Internal_ProjectTeamMembers_ProjectIdAndUserName", 1) { IsUnique = true }));

            HasRequired(x => x.Project).WithMany(x => x.ProjectTeamMemberss).HasForeignKey(x => x.ProjectId).WillCascadeOnDelete(false);
        }
    }
}
