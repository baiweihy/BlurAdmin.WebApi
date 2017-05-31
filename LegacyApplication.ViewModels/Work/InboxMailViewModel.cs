using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Shared.ByModule.Work.Enums;

namespace LegacyApplication.ViewModels.Work
{
    public class InboxMailViewModel
    {
        public int Id { get; set; }
        public string FromUserName { get; set; }
        public string FromPersonName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime SendTime { get; set; }
        public bool FromHasDeleted { get; set; }
        public MailType MailType { get; set; }
        public string MailTypeDisplay => MailType.ToString();

        public int ReceiveId { get; set; }
        public string ReceiveUserName { get; set; }
        public string ReceivePersonName { get; set; }
        public bool HasRead { get; set; }
        public DateTime? ReadTime { get; set; }
        public bool ReceiveHasDeleted { get; set; }

        public bool HasAttachments { get; set; }
    }
}
