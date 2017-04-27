using System.Collections.Generic;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Shared.Features.Tree
{
    public class TreeEntityBase<T>: EntityBase, ITreeEntity<T> where T: TreeEntityBase<T>
    {
        public int? ParentId { get; set; }
        public string AncestorIds { get; set; }
        public bool IsAbstract { get; set; }
        public int Level => AncestorIds?.Split('-').Length ?? 0;
        public T Parent { get; set; }
        public ICollection<T> Children { get; set; }
    }
}
