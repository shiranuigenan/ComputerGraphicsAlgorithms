using ComputerGraphicsAlgorithms;

var a = imageGenerate.ViewfinityS9();
var b = common.pixelsToBitmap(a);

//for (var i = 0; i < 10; i++)
//    common.saveJpeg(b, 91 + i, $"fifty{90 + i}.jpg");

//common.saveJpeg(b, 100, "fifty.jpg");
b.Save("fifty.png");
