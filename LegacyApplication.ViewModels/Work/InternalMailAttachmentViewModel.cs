using LegacyApplication.Shared.Features.Base;
using LegacyApplication.Shared.Features.File;

namespace LegacyApplication.ViewModels.Work
{
    public class InternalMailAttachmentViewModel : EntityBase, IFileEntity
    {
        public int MailId { get; set; }
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }
}
