using System.Collections.Generic;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Shared.Features.Tree
{
    public interface ITreeEntity<T> where T: EntityBase
    {
        int? ParentId { get; set; }
        string AncestorIds { get; set; }
        bool IsAbstract { get; set; }
        int Level { get; }
        T Parent { get; set; }
        ICollection<T> Children { get; set; }
    }
}
