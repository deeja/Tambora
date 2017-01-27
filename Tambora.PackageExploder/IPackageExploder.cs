namespace Tambora.PackageExploder
{
    using System;
    using System.Threading.Tasks;

    public interface IPackageExploder
    {
        event EventHandler<PackageProcessingArgs> ProcessingStarted;

        event EventHandler ProcessingFinished;

        Task<PackageItem[]> ExplodePackage(string fileName);
    }
}