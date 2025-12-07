using ComputerGraphicsAlgorithms;

var pixels = imageGenerate.Hat();
var bitmap=common.pixelsToBitmap(pixels);
bitmap.Save("1.png");
