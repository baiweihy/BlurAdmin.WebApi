using System.Data.Entity.ModelConfiguration;

namespace LegacyApplication.Base
{
    public class EntityBaseConfiguration<T> : EntityTypeConfiguration<T> where T : class, IEntityBase
    {
        public EntityBaseConfiguration()
        {
            HasKey(e => e.Id);
            Property(x => x.CreateTime).IsRequired();
            Property(x => x.UpdateTime).IsRequired();
            Property(x => x.CreateUser).IsRequired().HasMaxLength(50);
            Property(x => x.UpdateUser).IsRequired().HasMaxLength(50);
            Property(x => x.LastAction).IsRequired().HasMaxLength(50);
            Ignore(x => x.StatusDisplay);
        }
    }
}