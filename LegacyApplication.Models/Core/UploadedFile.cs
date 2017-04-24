using LegacyApplication.Base;
using LegacyApplication.Shared.Interfaces;

namespace LegacyApplication.Models.Core
{
    public class UploadedFile : EntityBase, IFileEntity
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

    public class UploadedFileConfiguration : EntityBaseConfiguration<UploadedFile>
    {
        public UploadedFileConfiguration()
        {
            Ignore(x => x.FileId);
            Property(x => x.FileName).HasMaxLength(200).IsRequired();
            Property(x => x.Path).HasMaxLength(200).IsRequired();
        }
    }
}