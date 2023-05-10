using SkiaSharp;

const string Source = "../../Samples/example.png";

// NOTE: SkiaSharp does not support TIFF file!
var signatureBitmap = SKBitmap.Decode(Source);
var time = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";
using var paint = new SKPaint{
    TextSize =  34,
    FakeBoldText = true,
    Color = new SKColor(255, 0, 0, 65),
    TextAlign = SKTextAlign.Center,
    Typeface = SKTypeface.FromFamilyName("Courier New",
                                         SKFontStyleWeight.Bold,
                                         SKFontStyleWidth.Normal,
                                         SKFontStyleSlant.Upright),
    IsAntialias = true,
    IsStroke = false
};

var height = signatureBitmap.Height;
var width = signatureBitmap.Width;
var imageCenter = (X: width / 2, Y: height / 2);
var oneThirdY = height / 3;
var twoThirdY = height * 2 / 3;

var canvas = new SKCanvas(signatureBitmap);
canvas.RotateDegrees(-45, imageCenter.X, imageCenter.Y);
canvas.DrawText(@"John Dandy Doe", new SKPoint(imageCenter.X, oneThirdY-25), paint);
canvas.DrawText(time, new SKPoint(imageCenter.X, oneThirdY+25), paint);
canvas.DrawText(@"John Dandy Doe 2", new SKPoint(imageCenter.X, twoThirdY-25), paint);
canvas.DrawText(time, new SKPoint(imageCenter.X, twoThirdY+25), paint);
canvas.RotateDegrees(45, imageCenter.X, imageCenter.Y);

var resultImage = SKImage.FromBitmap(signatureBitmap);
var data = resultImage.Encode(SKEncodedImageFormat.Png, 100);
using var fileStream = new FileStream("./output.png", FileMode.Create);
data.SaveTo(fileStream);