namespace Tambora.PackageExploder.Exceptions
{
    using System;

    public class FileNotValidPackageException:Exception
    {
        public FileNotValidPackageException(string message)
            : base(message)
        {
        }
    }
}