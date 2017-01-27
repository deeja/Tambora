using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tambora.PackageExploder.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    using Moq;

    using NUnit.Framework;

    using Tambora.PackageExploder.Exceptions;

    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PackageExploder_ExplodePackage
    {
        private PackageExploder exploder;

        private Mock<IPackageValidator> validator;

        private Mock<IPackageLoader> packageLoader;

        [SetUp]
        public void SetUp()
        {
            validator = new Mock<IPackageValidator>();
            packageLoader = new Mock<IPackageLoader>();
            exploder = new PackageExploder(validator.Object, packageLoader.Object);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public async void FileDoesntExist_FileNotFoundThrown()
        {
            var testingString = "testing string";
            this.validator.Setup(packageValidator => packageValidator.IsFileExtensionValid(It.IsAny<string>()))
                .Returns(true);
            validator.Setup(packageValidator => packageValidator.FileExists(testingString)).Returns(false);
            var packageItems = await this.exploder.ExplodePackage(testingString);
        }

        /// <summary>
        /// Test the file extensions. If the file is not a valid extension, then the FileNotFound should occur
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isAccepted"></param>
        [TestCase("C:\\something\\file.zip", true)]
        [TestCase("C:\\something\\lala.update", false)] // to be accepted in the future
        [TestCase("C:\\something\\lala.jpg", false)]
        public async void File_ValidExtension(string file, bool isAccepted)
        {
            this.validator.Setup(packageValidator => packageValidator.IsFileExtensionValid(file)).Returns(isAccepted);
            validator.Setup(packageValidator => packageValidator.FileExists(It.IsAny<string>())).Returns(true);
            try
            {
                await exploder.ExplodePackage(file);
            }
            catch (Exception exception)
            {
                if (!isAccepted)
                {
                    Assert.IsInstanceOf<FileExtensionNotAcceptedException>(exception);
                }
                return;
            }

            Assert.Fail("Error should have been thrown");
        }

        [Test]
        [ExpectedException(typeof(FileNotValidPackageException))]
        public async void ProvidedPackageIsNotValid()
        {
            this.validator.Setup(packageValidator => packageValidator.IsFileExtensionValid(It.IsAny<string>()))
                .Returns(true);
            validator.Setup(packageValidator => packageValidator.FileExists(It.IsAny<string>())).Returns(true);

            string filename = "a test file";
            this.packageLoader.Setup(loader => loader.LoadPackage(filename))
                .Throws(new FileNotValidPackageException("this is a test"));
            
            await exploder.ExplodePackage(filename);
        }
    }
}