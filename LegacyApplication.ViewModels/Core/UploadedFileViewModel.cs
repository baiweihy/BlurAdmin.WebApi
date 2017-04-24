using LegacyApplication.Base;
using LegacyApplication.Shared.Interfaces;

namespace LegacyApplication.ViewModels.Core
{
    public class UploadedFileViewModel : EntityBase, IFileEntity
    {
        public int FileId
        {
            get { return Id; }
            set { }
        }

        public string FileName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public bool Deleted { get; set; }
    }
}