namespace Tambora.PackageExploder
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

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

        public async Task<PackageItem[]> ExplodePackage(string fileName)
        {
            if (!packageValidator.IsFileExtensionValid(fileName))
            {
                throw new FileExtensionNotAcceptedException($"{fileName} does not have a valid extension");
            }

            if (!packageValidator.FileExists(fileName))
            {
                throw new FileNotFoundException($"Couldn't find the file {fileName}", fileName);
            }

            if (!this.packageValidator.IsPackageValid(fileName))
            {
                throw new FileNotValidPackageException($"{fileName} is not a valid package");
            }

            return new PackageItem[0];
            return null;

        }
    }
}