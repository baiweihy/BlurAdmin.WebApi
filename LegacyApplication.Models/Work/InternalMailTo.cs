using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApplication.Shared.Features.Base;

namespace LegacyApplication.Models.Work
{
    public class InternalMailTo : EntityBase
    {
        public int MailId { get; set; }
        public string UserName { get; set; }
        public string PersonName { get; set; }
        public bool HasRead { get; set; }
        public DateTime? ReadTime { get; set; }
        public bool HasDeleted { get; set; }
        public virtual InternalMail Mail { get; set; }
    }

    public class InternalMailToConfiguration : EntityBaseConfiguration<InternalMailTo>
    {
        public InternalMailToConfiguration()
        {
            ToTable("work.InternalMailTo");

            Property(x => x.UserName).IsRequired().HasMaxLength(50);
            Property(x => x.PersonName).HasMaxLength(100);

            HasRequired(x => x.Mail).WithMany(x => x.Tos).HasForeignKey(x => x.MailId).WillCascadeOnDelete(false);
        }
    }
}
