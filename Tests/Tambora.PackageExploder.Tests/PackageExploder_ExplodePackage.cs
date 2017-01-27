using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tambora.PackageExploder.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    using NUnit.Framework;

    using Tambora.PackageExploder.Exceptions;

    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PackageExploder_ExplodePackage
    {
        PackageExploder exploder = new PackageExploder();


        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void FileDoesntExist_FileNotFoundThrown()
        {
            exploder.ExplodePackage("X:/doesntexist");
        }

        /// <summary>
        /// Test the file extensions. If the file is not a valid extension, then the FileNotFound should occur
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isAccepted"></param>
        [TestCase("C:\\something\\file.zip", true)]
        [TestCase("C:\\something\\lala.update", false)] // to be accepted in the future
        [TestCase("C:\\something\\lala.jpg", false)]
        public void File_ValidExtension(string file, bool isAccepted)
        {
            try
            {
                exploder.ExplodePackage(file);
            }
            catch (Exception exception)
            {
                if (isAccepted)
                {
                    Assert.IsInstanceOf<FileNotFoundException>(exception);
                    return;
                }

                Assert.IsInstanceOf<FileExtensionNotAcceptedException>(exception);
                return;
            }

            Assert.Fail("Error should have been thrown");
        }
    }
}
