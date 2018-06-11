# RasterizePdfComponent
The "RasterizePdfComponent" is a component designed for pdf file rasterization.

## Features
It has simple interface where you initialize object with initial file path and just call convert method. Rasterization quality and output path can also be optionally specified.

## Inside
This component uses [Ghostscript.NET](https://github.com/jhabjan/Ghostscript.NET) for pdf to image conversion and [itextsharp](https://github.com/itext/itextsharp) to combine images to final pdf file.

## Component uses:
* teachers can make their pdf material non searchable / non copyable;
* a way to simplify overcomplicated vectorized pdf's.

## How to
1. Make sure you have native [ghostscript](https://www.ghostscript.com/download/gsdnld.html) library (gsdll32.dll, version >= 9.23) installed
