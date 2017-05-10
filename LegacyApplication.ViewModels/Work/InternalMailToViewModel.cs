using System;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.ViewModels.Work
{
    public class InternalMailToViewModel : EntityBase
    {
        public int MailId { get; set; }
        public string UserName { get; set; }
        public string PersonName { get; set; }
        public bool HasRead { get; set; }
        public DateTime? ReadTime { get; set; }
        public bool HasDeleted { get; set; }
        public virtual InternalMailViewModel Mail { get; set; }
    }
}
