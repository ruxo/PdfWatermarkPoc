using ImageMagick;

const string Source = "../../Samples/example.tif";

using var image = new MagickImage(Source);

var heightRelativedFontSize = image.Height * 0.038;
var shiftOffset = heightRelativedFontSize * 2;

var drawingText = new Drawables();
var text = $"John Dandy Doe\n{DateTime.Now:dd/MM/yyyy HH:mm::ss}";
//ordering ของสร้าง Drawables มีผล ต้องเรียงให้ถูก
drawingText.Font("Courier New", FontStyleType.Normal, FontWeight.Bold, FontStretch.Normal)
           .FontPointSize(heightRelativedFontSize)
           .FillColor(MagickColors.Red) //new MagickColor(255, 179, 176)
           .FillOpacity(new Percentage(25))
           .Gravity(Gravity.Center)
           .Rotation(-45)
           .Text(shiftOffset, -shiftOffset, text)
           .Text(-shiftOffset, shiftOffset, text);
        
image.Draw(drawingText);
image.Write("./output.jpg", MagickFormat.Jpeg);