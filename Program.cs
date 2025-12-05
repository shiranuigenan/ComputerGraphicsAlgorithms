using ComputerGraphicsAlgorithms;

var pixels = common.Scale16(imageGenerate.PerfectBall());
var bitmap=common.pixelsToBitmap(pixels);
bitmap.Save("1.png");
