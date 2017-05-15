using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Shared.ByModule.Work.Enums;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Work
{
    public class InternalMail : EntityBase
    {
        public string UserName { get; set; }
        public string PersonName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime SendTime { get; set; }
        public bool HasDeleted { get; set; }
        public MailType MailType { get; set; }

        public virtual ICollection<InternalMailTo> Tos { get; set; }
        public virtual ICollection<InternalMailAttachment> Attachments { get; set; }
    }

    public class InternalMailConfiguration : EntityTypeConfiguration<InternalMail>
    {
        public InternalMailConfiguration()
        {
            ToTable("work.InternalMail");

            Property(x => x.UserName).IsRequired().HasMaxLength(50);
            Property(x => x.PersonName).HasMaxLength(100);

            Property(x => x.Title).IsRequired().HasMaxLength(200);
            Property(x => x.Body).HasColumnType("nvarchar(max)");
        }
    }
}
