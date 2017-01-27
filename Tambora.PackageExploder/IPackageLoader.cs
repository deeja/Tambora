namespace Tambora.PackageExploder
{
    using System.Threading.Tasks;

    public interface IPackageLoader
    {
        Task<PackageEntrySink> LoadPackage(string filename);
    }
}