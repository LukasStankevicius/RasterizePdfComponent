using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;
using Ghostscript.NET.Rasterizer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;


namespace RasterizePdfComponent
{
    /// <summary>
    /// The main <c>RasterizePdf</c> class.
    /// Contains all methods for organizing rasterization components.
    /// </summary>
    public class RasterizePdf : IRasterizePdf
    {
        private List<string> imageList;
        private string tempDirectory;
        private string InputFilePath;
        /// <summary>
        /// The <c>OutputFilePath</c> attribute.
        /// Shows the destination for the final rasterized file.
        /// Contains full path and file name
        /// </summary>
        /// <example>
        /// <code>
        /// public string OutputFilePath = @"E:\MainTestFolder\TestPdf.pdf";
        /// </code>
        /// </example>
        public string OutputFilePath;
        /// <summary>
        /// The <c>dpi</c> attribute.
        /// Contains resolution in dpi for final rasterized pdf.
        /// Can be no less than 1 or no greater than 600. If bound exceeded, the bound value is given
        /// </summary>
        public int Dpi = 100;
        /// <summary>
        /// The <c>RasterizePdf</c> class constructor.
        /// Checks for valid input file path and saves it as temporary
        /// Gets default output filepath.
        /// </summary>
        public RasterizePdf(string inputFilePath)
        {
            // We need to deal with input paths which have special unicode characters:
            // https://github.com/jhabjan/Ghostscript.NET/issues/3
            if (!File.Exists(inputFilePath))
            { throw new ArgumentException("Invalid input file path"); }
            InputFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + "a.pdf");
            File.Copy(inputFilePath, InputFilePath);
            // otherwise it would be faster just to do:
            //InputFilePath = inputFilePath;
            OutputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), Path.GetFileNameWithoutExtension(inputFilePath)+"_Rasterized.pdf");
        }
        /// <summary>
        /// Interface method.
        /// Does bounding for <c>dpi</c> attribute, checks output file path.
        /// Create temporary folder for images.
        /// Do actual conversion.
        /// Cleans all temporary files.
        /// </summary>
        public void Convert()
        {
            if (Dpi > 600){Dpi = 600;}
            else if (Dpi < 1) { Dpi = 1; }
            CheckOutputFilePath();
            tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            if (!Directory.Exists(tempDirectory)){Directory.CreateDirectory(tempDirectory);}
            
            Pdf2Jpegs();
            imageList = new List<string>(System.IO.Directory.GetFiles(tempDirectory));
            ConvertImagesToPdf();

            CleareCreated();
        }
        private void Pdf2Jpegs()
        {
            File.OpenRead(InputFilePath);
            using (GhostscriptRasterizer rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(InputFilePath);
                for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                {
                    string pageFilePath = Path.Combine(tempDirectory, string.Format("Page--{0:000}.png", pageNumber));

                    var img = rasterizer.GetPage(Dpi, Dpi, pageNumber);
                    img.Save(pageFilePath, ImageFormat.Jpeg);
                }
            }
        }

        private void ConvertImagesToPdf()
        {
            if (imageList == null || imageList.Count == 0) { CleareCreated(); throw new ArgumentException("Problems with Ghostscript, check if you have newest version"); }
            using (var outputStream = new MemoryStream())
            {
                int pageCount = 0;

                using (Document document = new Document())
                {
                    document.SetMargins(0, 0, 0, 0);

                    PdfWriter.GetInstance(document, outputStream).SetFullCompression();
                    document.Open();

                    foreach (string sourceFilePath in imageList)
                    {
                        iTextSharp.text.Rectangle pageSize = null;

                        using (var sourceImage = new Bitmap(sourceFilePath))
                        {
                            pageSize = new iTextSharp.text.Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
                        }

                        document.SetPageSize(pageSize);
                        document.NewPage();

                        using (var ms = new MemoryStream())
                        {
                            var image = iTextSharp.text.Image.GetInstance(sourceFilePath);
                            document.Add(image);
                            ++pageCount;
                        }
                    }
                }

                File.WriteAllBytes(OutputFilePath, outputStream.ToArray());
            }
        }

        private void CleareCreated()
        {
            if (Directory.Exists(tempDirectory)) { Directory.Delete(tempDirectory, true); }
            if (File.Exists(InputFilePath)) { File.Delete(InputFilePath); }
        }
        private void CheckOutputFilePath()
        {
            if (string.IsNullOrWhiteSpace(OutputFilePath)) { CleareCreated(); throw new ArgumentException("Invalid output file path"); }
            if (OutputFilePath.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0 ||
                Path.GetFileName(OutputFilePath).IndexOfAny(Path.GetInvalidFileNameChars()) > 0)
            { CleareCreated(); throw new ArgumentException("Output path has invalid characters!"); }
            if (!Directory.Exists(Path.GetDirectoryName(OutputFilePath)))
            { CleareCreated(); throw new ArgumentException("Specified output path directory does't exists"); }
            if (Path.GetExtension(OutputFilePath) != ".pdf")
            { CleareCreated(); throw new ArgumentException("Invalid output file extension, must be '.pdf' "); }
        }
    }
}
