using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using LegacyApplication.Shared.ByModule.HumanResources.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.HumanResources
{
    public class AdministrativePost : EntityBase
    {
        public string Name { get; set; }
        public AdministrativePostNature AdministrativePostNature { get; set; }

        public int AdministrativeLevelId { get; set; }
        public AdministrativeLevel AdministrativeLevel { get; set; }
    }

    public class AdministrativePostConfiguration : EntityBaseConfiguration<AdministrativePost>
    {
        public AdministrativePostConfiguration()
        {
            ToTable("hr.AdministrativePost");

            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));
            HasRequired(x => x.AdministrativeLevel).WithMany(x => x.AdministrativePosts).HasForeignKey(x => x.AdministrativeLevelId);
        }
    }
}
