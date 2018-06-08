using System;
using RasterizePdfComponent;

namespace User
{
    class Program
    {
        static void Main(string[] args)
        {
            string pdfToConvertPath = @"E:\MainTestFolder\TestPdf.pdf";
            RasterizePdf sample = new RasterizePdf(pdfToConvertPath);
            sample.Convert();
        }
    }
}
