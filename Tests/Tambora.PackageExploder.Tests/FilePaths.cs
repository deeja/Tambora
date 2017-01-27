namespace Tambora.PackageExploder.Tests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    public static class FilePaths
    {
        public static readonly string InvalidPackageFilename = Path.Combine(
            Environment.CurrentDirectory,
            "Resources\\NotAValidPackage.zip");

        public static readonly string ValidPackageFilename = Path.Combine(
            Environment.CurrentDirectory,
            "Resources\\ValidPackageFilesItems.zip");
    }
}