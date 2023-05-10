using System.Diagnostics;
using GdPicture14;
using GdPicture14.Imaging;

const string Source = "../../Samples/example.pdf";
const string stampingText = "John Dandy Doe";
var stampingTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm::ss");

new LicenseManager().RegisterKEY("0460588278467898272281748"); //Trial Key

using var gdpicturePDF = new GdPicturePDF();
var status = gdpicturePDF.LoadFromFile(Source);
Debug.Assert(status == GdPictureStatus.OK);
gdpicturePDF.SetOrigin(PdfOrigin.PdfOriginBottomLeft);
gdpicturePDF.SetMeasurementUnit(PdfMeasurementUnit.PdfMeasurementUnitMillimeter);

var fontName = gdpicturePDF.AddStandardFont(PdfStandardFont.PdfStandardFontCourierBold);
var halfOfTextWidth = gdpicturePDF.GetTextWidth(fontName, 34, stampingText) / 2;
var shiftingTextOffset = (float)(halfOfTextWidth / Math.Sqrt(2));

var halfOfTimeWidth = gdpicturePDF.GetTextWidth(fontName, 34, stampingTime) / 2;
var shiftingTimeOffset = (float)(halfOfTimeWidth / Math.Sqrt(2));

//For A4-Sized PDF
var ROTATED_DEGREE = 45F;
var FIRSTLINE_TEXT_HEIGHTSHIFTOFFSET = 60F;
var FIRSTLINE_TIME_HEIGHTSHIFTOFFSET = 35F;
var TIME_ADJUSTED_DISTANCE = (FIRSTLINE_TEXT_HEIGHTSHIFTOFFSET - FIRSTLINE_TIME_HEIGHTSHIFTOFFSET)/2F;
var LINEDISTANCE = 100F;
//For A4-Sized PDF

int pageCount = gdpicturePDF.GetPageCount();
for (var pageNo = 1; pageNo <= pageCount; pageNo++) {
    gdpicturePDF.SelectPage(pageNo);
    gdpicturePDF.NormalizePage();
    
    float pageWidth = gdpicturePDF.GetPageWidth();
    float pageHeight = gdpicturePDF.GetPageHeight();
    var centerPoint = (X: pageWidth / 2, Y: pageHeight / 2);
    
    gdpicturePDF.SetFillColor(GdPictureColor.Red);
    gdpicturePDF.SetFillAlpha(65);
    gdpicturePDF.SetTextSize(34);
    gdpicturePDF.DrawRotatedText(fontName,
                                 centerPoint.X - shiftingTextOffset,
                                 centerPoint.Y - shiftingTextOffset + FIRSTLINE_TEXT_HEIGHTSHIFTOFFSET,
                                 stampingText,
                                 ROTATED_DEGREE);
    gdpicturePDF.DrawRotatedText(fontName,
                                 centerPoint.X - shiftingTimeOffset + TIME_ADJUSTED_DISTANCE,
                                 centerPoint.Y - shiftingTimeOffset + FIRSTLINE_TIME_HEIGHTSHIFTOFFSET + TIME_ADJUSTED_DISTANCE,
                                 stampingTime, ROTATED_DEGREE);

    gdpicturePDF.DrawRotatedText(fontName,
                                 centerPoint.X - shiftingTextOffset,
                                 centerPoint.Y - shiftingTextOffset + (FIRSTLINE_TEXT_HEIGHTSHIFTOFFSET - LINEDISTANCE),
                                 stampingText,
                                 ROTATED_DEGREE);
    gdpicturePDF.DrawRotatedText(fontName,
                                 centerPoint.X - shiftingTimeOffset + TIME_ADJUSTED_DISTANCE,
                                 centerPoint.Y - shiftingTimeOffset + (FIRSTLINE_TIME_HEIGHTSHIFTOFFSET - LINEDISTANCE) + TIME_ADJUSTED_DISTANCE,
                                 stampingTime,
                                 ROTATED_DEGREE);
}

gdpicturePDF.SaveToFile("./output.pdf");