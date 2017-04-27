using System.Collections.Generic;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Shared.Features.Tree
{
    public class TreeEntityBaseConfiguration<T> : EntityBaseConfiguration<T> where T : TreeEntityBase<T>
    {
        public TreeEntityBaseConfiguration()
        {
            Property(x => x.AncestorIds).HasMaxLength(200);
            Ignore(x => x.Level);

            HasOptional(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId).WillCascadeOnDelete(false);
        }
    }
}
