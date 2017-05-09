using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using LegacyApplication.Shared.ByModule.HumanResources.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.HumanResources
{
    public class TitlePost : EntityBase
    {
        public string Name { get; set; }
        public TitlePostNature TitlePostNature { get; set; }

        public int TitleLevelId { get; set; }
        public TitleLevel TitleLevel { get; set; }
    }

    public class TitlePostConfiguration : EntityBaseConfiguration<TitlePost>
    {
        public TitlePostConfiguration()
        {
            ToTable("hr.TitlePost");

            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));
            HasRequired(x => x.TitleLevel).WithMany(x => x.TitlePosts).HasForeignKey(x => x.TitleLevelId);
        }
    }
}
