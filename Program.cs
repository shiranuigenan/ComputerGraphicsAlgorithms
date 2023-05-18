
using ComputerGraphicsAlgorithms;

var m = new Mp4Render(16384, 9216, 256, 60);
m.OnFrameRender += (frameNumber, frameBuffer) =>
{
    var i = 0;
    for (int h = 0; h < m.Height; h++)
        for (int w = 0; w < m.Width; w++)
        {
            var r = 65535 - w - h - 256 * frameNumber;
            var g = 45568 - w - h - 256 * frameNumber;
            var b = 25600 - w - h - 256 * frameNumber;

            frameBuffer[i++] = ((byte)r);
            frameBuffer[i++] = ((byte)(r >> 8));
            frameBuffer[i++] = ((byte)g);
            frameBuffer[i++] = ((byte)(g >> 8));
            frameBuffer[i++] = ((byte)b);
            frameBuffer[i++] = ((byte)(b >> 8));
        }
};
m.RenderToFile("2.mp4");
