using System;
using System.Collections.Generic;
using LegacyApplication.Shared.ByModule.Work.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Work
{
    public class InternalMailViewModel : EntityBase
    {
        public string UserName { get; set; }
        public string PersonName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime SendTime { get; set; }
        public bool HasDeleted { get; set; }
        public MailType MailType { get; set; }

        public virtual ICollection<InternalMailToViewModel> Tos { get; set; }
        public virtual ICollection<InternalMailAttachmentViewModel> Attachments { get; set; }
    }
}
