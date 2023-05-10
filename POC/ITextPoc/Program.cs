using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Layout;
using iText.Layout.Element;

const string Source = "../../Samples/example.pdf";

using var pdf = new PdfDocument(new PdfReader(Source), new PdfWriter("./output.pdf"));

var doc = new Document(pdf);
var font = PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD);

var paragraph = new Paragraph().SetFont(font).SetFontSize(34);
paragraph.Add(new Text($"John Dandy Doe\n     {DateTime.Now:dd-MM-yyyy HH:mm:ss}     "));
paragraph.SetMultipliedLeading(1.2f);

var gs1 = new PdfExtGState().SetFillOpacity(0.25f);
    
// Implement transformation matrix usage in order to scale image
for (var i = 1; i <= pdf.GetNumberOfPages(); i++) 
{
    var pdfPage = pdf.GetPage(i);
    var pageSize = pdfPage.GetPageSizeWithRotation();
        
    // When "true": in case the page has a rotation, then new content will be automatically rotated in the
    // opposite direction. On the rotated page this would look as if new content ignores page rotation.
    pdfPage.SetIgnorePageRotationForContent(true);
    var x = (pageSize.GetLeft() + pageSize.GetRight()) / 2;
    var y = (pageSize.GetTop() + pageSize.GetBottom()) / 2;
    var over = new PdfCanvas(pdf.GetPage(i));
    over.SetFillColor(ColorConstants.RED);
    over.SaveState();
    over.SetExtGState(gs1);
    doc.ShowTextAligned(paragraph, x-30, y+150, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 45);
    doc.ShowTextAligned(paragraph, x-30, y-150, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.TOP, 45);
    
    over.RestoreState();
}
    
doc.Close();