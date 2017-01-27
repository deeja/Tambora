namespace Tambora.PackageExploder
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Exceptions;

    public class PackageExploder: IPackageExploder
    {
        private readonly IPackageValidator packageValidator;

        private readonly IPackageLoader packageLoader;

        public PackageExploder(IPackageValidator packageValidator, IPackageLoader packageLoader)
        {
            this.packageValidator = packageValidator;
            this.packageLoader = packageLoader;
        }

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

            var packageEntrySink = this.packageLoader.LoadPackage(fileName);

            return new PackageItem[0];
        }
    }
}