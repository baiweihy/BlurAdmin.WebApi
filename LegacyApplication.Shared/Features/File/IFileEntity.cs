namespace LegacyApplication.Shared.Features.File
{
    public interface IFileEntity
    {
        int FileId { get; set; }
        string FileName { get; set; }
        string Path { get; set; }
        long Size { get; set; }
    }
}
