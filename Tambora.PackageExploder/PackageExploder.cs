namespace Tambora.PackageExploder
{
    using System;
    using System.IO;

    using Tambora.PackageExploder.Exceptions;

    public class PackageExploder: IPackageExploder
    {
        public event EventHandler<PackageExplodedArgs> PackageExploded;

        public event EventHandler<PackageProcessingArgs> ProcessingStarted;

        public event EventHandler ProcessingFinished;

        public void ExplodePackage(string safeFileName)
        {
            if (!".zip".Equals(Path.GetExtension(safeFileName), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FileExtensionNotAcceptedException($"{safeFileName} does not have a valid extension");
            }

            if (!File.Exists(safeFileName))
            {
                throw new FileNotFoundException("Couldn't find the file "+ safeFileName, safeFileName);
            }
        }
    }
}