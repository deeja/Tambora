namespace Tambora
{
    using System;

    public interface IPackageExploder
    {
        event EventHandler<PackageExplodedArgs> PackageExploded;

        event EventHandler<PackageProcessingArgs> ProcessingStarted;

        event EventHandler ProcessingFinished;

        void ExplodePackage(string safeFileName);
    }
}