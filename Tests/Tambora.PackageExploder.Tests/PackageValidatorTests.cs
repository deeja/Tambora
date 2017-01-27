namespace Tambora.PackageExploder.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class PackageValidatorTests
    {
        PackageValidator validator = new PackageValidator();

        [TestCase("c:\\something\\file.zip", true)]
        [TestCase("c:\\something\\twofile.ZiP", true)]
        [TestCase("threefile.ZiP", true)]
        [TestCase("c:\\something\\file.update", false)]
        [TestCase("c:\\something\\file.zipp", false)]
        [TestCase("c:\\something\\file.zip.jpg", false)]
        public void IsFileExtensionValid_ValidateExtension(string fileName, bool valid)
        {
            var result = validator.IsFileExtensionValid(fileName);
            Assert.AreEqual(valid, result);
        }

        [Test]
        public void FileExists_FileActuallyExist()
        {
            var result = validator.FileExists(FilePaths.ValidPackageFilename);
            Assert.IsTrue(result);
        }

        [Test]
        public void FileExists_FileDoesntExist()
        {
            var result = validator.FileExists("S:\\no file here");
            Assert.IsFalse(result);
        }


    }
}