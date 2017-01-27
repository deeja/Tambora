namespace Tambora.PackageExploder
{
    public interface IPackageLoader
    {
        PackageEntrySink LoadPackage(string filename);
    }
}