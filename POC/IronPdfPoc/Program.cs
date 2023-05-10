using IronPdf.Editing;

const string Source = "../../Samples/example.pdf";

var pdf = PdfDocument.FromFile(Source);
var htmlText = @$"<div style='text-align: center; min-width: 1000px; color: red; font-family: courier; font-size: 34; font-weight: bold'>
                            <p style='margin-bottom: 5px'>John Dandy Doe</p>
                            <span>{DateTime.Now:dd-MM-yyyy HH:mm:ss}</span>
                          </div>";
        
var htmlStatmp1 = new HtmlStamper{
    Html = htmlText,
    Opacity = 25,
    Rotation = -45,
    HorizontalOffset = new Length(20)
};
var htmlStatmp2 = new HtmlStamper{
    Html = htmlText,
    Opacity = 25,
    Rotation = -45,
    HorizontalOffset = new Length(-20)
};

pdf.ApplyStamp(htmlStatmp1);
pdf.ApplyStamp(htmlStatmp2);
pdf.SaveAs("./output.pdf");