namespace Tambora.PackageExploder
{
    public interface IPackageValidator
    {
        bool IsFileExtensionValid(string filename);

        bool FileExists(string fileName);
    }
   
}