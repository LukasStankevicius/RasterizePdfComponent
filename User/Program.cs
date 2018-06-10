using System;
using RasterizePdfComponent;
using System.IO;

namespace User
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePathsRoot = @"E:\MainTestFolder\";
            string pdfToConvertPath;
            int num_of_pdf_files = 10;
            for (int i = 1; i <= num_of_pdf_files; i++)
            {
                pdfToConvertPath = Path.Combine(filePathsRoot, "FileNr"+i.ToString() + ".pdf");
                RasterizePdf sample = new RasterizePdf(pdfToConvertPath);
                sample.Convert();
            }
        }
    }
}
