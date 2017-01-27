namespace Tambora.PackageExploder.Exceptions
{
    using System;

    public class FileExtensionNotAcceptedException: Exception
    {
        public FileExtensionNotAcceptedException(string message): base(message)
        {
        }
    }
}