using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.HumanResources
{
    public class Nationality : EntityBase
    {
        public string Name { get; set; }
    }

    public class NationalityConfiguration : EntityBaseConfiguration<Nationality>
    {
        public NationalityConfiguration()
        {
            ToTable("hr.Nationality");
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasMaxLength(50).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }
    }
}
