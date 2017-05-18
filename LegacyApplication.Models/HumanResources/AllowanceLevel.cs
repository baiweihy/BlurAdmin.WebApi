using LegacyApplication.Shared.Features.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApplication.Models.HumanResources
{
    public class AllowanceLevel : EntityBase
    {
        public string Name { get; set; }
    }

    public class AllowanceLevelConfiguration:EntityBaseConfiguration<AllowanceLevel>
    {
        public AllowanceLevelConfiguration()
        {
            ToTable("hr.AllowanceLevel");
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Name).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute {
                    IsUnique=true
                })
                );
        }

    }
}
