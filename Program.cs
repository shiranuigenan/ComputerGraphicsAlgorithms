using ComputerGraphicsAlgorithms;

var pixels = imageGenerate.W2560H1440();
var bitmap = common.pixelsToBitmap(pixels);

bitmap.Save("2.png");