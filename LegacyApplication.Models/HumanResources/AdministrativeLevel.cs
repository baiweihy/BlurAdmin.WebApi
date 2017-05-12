using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using LegacyApplication.Shared.ByModule.HumanResources.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.HumanResources
{
    public class AdministrativeLevel : EntityBase
    {
        public string Name { get; set; }
        public ICollection<AdministrativePost> AdministrativePosts { get; set; }
    }

    public class AdministrativeLevelConfiguraton : EntityBaseConfiguration<AdministrativeLevel>
    {
        public AdministrativeLevelConfiguraton()
        {
            ToTable("hr.AdministrativeLevel");

            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }
    }
}
