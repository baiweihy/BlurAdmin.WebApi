using System;
using LegacyApplication.Shared.ByModule.Work.Enums;

namespace LegacyApplication.ViewModels.Work
{
    public class SentMailViewModel: InternalMailViewModel
    {
        public bool HasAttachments { get; set; }
        public int AttachmentCount { get; set; }
        public int ToCount { get; set; }
        public bool AnyoneRead { get; set; }
        public bool AllRead { get; set; }
    }
}
