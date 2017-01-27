namespace Tambora.PackageExploder
{
    using System;
    using System.IO;
    

    public class PackageValidator : IPackageValidator
    {

        public bool IsFileExtensionValid(string filename)
        {
            return ".zip".Equals(Path.GetExtension(filename),StringComparison.InvariantCultureIgnoreCase);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}