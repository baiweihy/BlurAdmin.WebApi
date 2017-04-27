using LegacyApplication.Shared.Features.File;

namespace LegacyApplication.ViewModels.Core
{
    public class FileSimpleViewModel: IFileEntity
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }
}
