namespace Tambora.PackageExploder
{
    public interface IPackageValidator
    {
        bool IsPackageValid(string filename);

        bool IsFileExtensionValid(string filename);

        bool FileExists(string fileName);
    }
}