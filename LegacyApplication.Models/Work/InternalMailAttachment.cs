using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Shared.Features.Base;
using LegacyApplication.Shared.Features.File;

namespace LegacyApplication.Models.Work
{
    public class InternalMailAttachment : EntityBase, IFileEntity
    {
        public int MailId { get; set; }
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }

        public virtual InternalMail Mail { get; set; }
    }

    public class InternalMailAttachmentConfiguration : EntityBaseConfiguration<InternalMailAttachment>
    {
        public InternalMailAttachmentConfiguration()
        {
            ToTable("work.InternalMailAttachment");

            Property(x => x.FileName).HasMaxLength(200).IsRequired();
            Property(x => x.Path).HasMaxLength(200).IsRequired();

            HasRequired(x => x.Mail).WithMany(x => x.Attachments).HasForeignKey(x => x.MailId).WillCascadeOnDelete(false);
        }
    }
}
