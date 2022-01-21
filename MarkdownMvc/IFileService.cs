namespace MarkdownMvc
{
    public interface IFileService
    {
        void DeleteImage(string filename);
        void ProcessRemovedImages(string oldBody, string newBody);
    }
}