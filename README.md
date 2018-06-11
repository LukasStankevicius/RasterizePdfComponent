# RasterizePdfComponent
The "RasterizePdfComponent" is a component designed for pdf file rasterization.

## Features
It has simple interface where you initialize object with initial file path and just call convert method. Rasterization quality and output path can also be optionally specified.

## Inside
This component uses [Ghostscript.NET](https://github.com/jhabjan/Ghostscript.NET) for pdf to image conversion and [itextsharp](https://github.com/itext/itextsharp) to combine images to final pdf file.

## Component uses:
* teachers can make their pdf material non searchable / non copyable;
* a way to simplify overcomplicated vectorized pdf's.

## Installation guide
1. Make sure you have native [ghostscript](https://www.ghostscript.com/download/gsdnld.html) library (gsdll32.dll, version >= 9.23) installed
2. Add project from NuGet in Visual Studio:
   - Select your .NET project
   - Right click References
   - Click Manage NuGet Packages...
   - In Browse tab  search for RasterizePdfComponent
   - install RasterizePdfComponent and other associate packages

## At its simplest:
```
using RasterizePdfComponent;
```
```
RasterizePdf sample = new RasterizePdf(@"E:\MainTestFolder\MyTestPdf.pdf");
sample.Convert();
```
Converted file will have dpi=100 and be located at E:\MainTestFolder\MyTestPdf_Rasterized.pdf

For more detailed example please look [here](https://github.com/LukasStankevicius/RasterizePdfComponent/blob/master/User/Program.cs)
## Available on:
* GitHub: https://github.com/LukasStankevicius/RasterizePdfComponent
* NuGet: https://www.nuget.org/packages/RasterizePdfComponent/
