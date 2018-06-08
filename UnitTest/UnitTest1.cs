using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RasterizePdfComponent;

namespace UnitTest
{
    [TestClass]
    public class RasterizePdfTests
    {
        /*  Folder structure with files TestPdf.pdf is needed:
         E:.
         +---MainTestFolder
             +---Test_folder_ÀÆèáëþ_
             |   \---TestPdf
             |       \---pdf
             +---Converted
             \---TestPdf
                 \---pdf
        */
        public string input = @"E:\MainTestFolder\TestPdf.pdf";
        public string bad_input = @"E:\MainTestFolder\Converted\TestPdf.pdf";
        public string input_with_characters = @"E:\MainTestFolder\Test_folder_ÀÆèáëþ_\TestPdf.pdf";
        public string SpecifiedOutputPath = @"E:\MainTestFolder\Converted\PdfRasterized.pdf";
        public string BadSpecifiedOutputPathFolder = @"E:\MainTestFolder\Converted\Converted\PdfRasterized.pdf";
        public string BadSpecifiedOutputPathExtension = @"E:\MainTestFolder\Converted\PdfRasterized.pdff";
        public int[] SpecifiedDpi = { 0, 100, 300, 1600 };

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Specified output path directory does't exists")]
        public void Convert_OutputPathSpecified_DoesNotExists()
        {
            // arrange
            RasterizePdf sample = new RasterizePdf(input);

            // act
            sample.OutputFilePath = BadSpecifiedOutputPathFolder;
            sample.Convert();

            // assert
            Assert.IsTrue(File.Exists(sample.OutputFilePath), "File does not exist");

            // clear new mess
            ClearMess(sample.OutputFilePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid output file extension, must be '.pdf' ")]
        public void Convert_OutputPathSpecified_BadExtension()
        {
            // arrange
            RasterizePdf sample = new RasterizePdf(input);

            // act
            sample.OutputFilePath = BadSpecifiedOutputPathExtension;
            sample.Convert();

            // assert
            Assert.IsTrue(File.Exists(sample.OutputFilePath), "File does not exist");

            // clear new mess
            ClearMess(sample.OutputFilePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid input file path")]
        public void Convert_BadInputPath()
        {
            // arrange
            RasterizePdf sample = new RasterizePdf(bad_input);

            // act
            sample.Convert();

            // assert
            Assert.IsTrue(File.Exists(sample.OutputFilePath), "File does not exist");

            // clear new mess
            ClearMess(sample.OutputFilePath);
        }

        [TestMethod]
        public void Convert_inputPathWithLithuanianLetters()
        {
            // arrange
            RasterizePdf sample = new RasterizePdf(input_with_characters);

            // act
            sample.Convert();

            // assert
            Assert.IsTrue(File.Exists(sample.OutputFilePath), "File does not exist");

            // clear new mess
            ClearMess(sample.OutputFilePath);
        }

        [TestMethod]
        public void Convert_WithDefaultParameters()
        {
            // arrange
            RasterizePdf sample = new RasterizePdf(input);

            // act
            sample.Convert();

            // assert
            Assert.IsTrue(File.Exists(sample.OutputFilePath), "File does not exist");

            // clear new mess
            ClearMess(sample.OutputFilePath);
        }

        [TestMethod]
        public void Convert_OutputPathSpecified()
        {
            // arrange
            RasterizePdf sample = new RasterizePdf(input);

            // act
            sample.OutputFilePath = SpecifiedOutputPath;
            sample.Convert();

            // assert
            Assert.AreEqual(SpecifiedOutputPath, sample.OutputFilePath, "Output directory not set");
            Assert.IsTrue(File.Exists(sample.OutputFilePath), "File does not exist");

            // clear new mess
            ClearMess(sample.OutputFilePath);
        }

        [TestMethod]
        
        public void Convert_DpiSpecified_tooHighOrTooLow()
        {
            foreach (int specifiedDpi in SpecifiedDpi) {
                // arrange
                RasterizePdf sample = new RasterizePdf(input);

                // act
                sample.Dpi = specifiedDpi; // 1 <= Dpi <= 600 otherwise fails
                sample.Convert();

                // assert
                Assert.IsTrue(File.Exists(sample.OutputFilePath), "File does not exist");

                // clear new mess
                ClearMess(sample.OutputFilePath);
            }
        }

        [TestMethod]

        private void ClearMess(string OutputFilePath)
        {
            if (File.Exists(OutputFilePath))
            {
                File.Delete(OutputFilePath);
            }
        }
    }
}
