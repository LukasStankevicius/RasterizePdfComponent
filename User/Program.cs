using System;
using RasterizePdfComponent;

namespace User
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = @"E:\MainTestFolder\MyTestFile.pdf";
            // OPTIONAL string ConvertedFilePath = @"E:\MainTestFolder\MyNewFile.pdf"; ;
            // OPTIONAL int dpi = 100; // if less than 1 or more than 600, dpi will be bounded
            RasterizePdf sample = new RasterizePdf(inputFilePath);
            // OPTIONAL sample.OutputFilePath = ConvertedFilePath;
            // OPTIONAL sample.Dpi = dpi;
            sample.Convert();
            // if default values used, converted file will be at @"E:\MainTestFolder\MyTestFile_Rasterized.pdf";
        }
    }
}
