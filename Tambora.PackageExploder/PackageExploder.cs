namespace Tambora.PackageExploder
{
    using System;
    using System.IO;

    using Exceptions;

    public class PackageExploder: IPackageExploder
    {
        private readonly IPackageValidator packageValidator;

        public PackageExploder(IPackageValidator packageValidator)
        {
            this.packageValidator = packageValidator;
        }

        public event EventHandler<PackageExplodedArgs> PackageExploded;

        public event EventHandler<PackageProcessingArgs> ProcessingStarted;

        public event EventHandler ProcessingFinished;

        public void ExplodePackage(string fileName)
        {
            if (!".zip".Equals(Path.GetExtension(fileName), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FileExtensionNotAcceptedException($"{fileName} does not have a valid extension");
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Couldn't find the file {fileName}", fileName);
            }

            if (!packageValidator.IsPackageValid(fileName))
            {
                throw new FileNotValidPackageException($"{fileName} is not a valid package");
            }
        }
    }
}