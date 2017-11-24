namespace FileStoringWithDependency.IFileStoreAndLoad
{
    public interface IFileStoreAndLoad
    {

        void SaveText(string filename, string text);

        string LoadText(string filename);
    }
}
