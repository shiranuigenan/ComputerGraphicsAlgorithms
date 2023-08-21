using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsAlgorithms
{
    public static class VideoGenerate
    {
        public static void Mp4RendererTest()
        {
            var m = new Mp4Render(16384, 9216, 5, 60);
            m.OnFrameRender += (frameNumber, frameBuffer) =>
            {
                var i = 0;
                for (int h = 0; h < m.InHeight; h++)
                    for (int w = 0; w < m.InWidth; w++)
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
        }
        public static void Rgb8()
        {
            int k = 0;
            var m = new Mp4Render(45, 80, 3600, 60, 4320, 7680, "rgb8", 1);
            m.OnFrameRender += (frameNumber, frameBuffer) =>
            {
                frameBuffer[frameNumber] = (byte)(k/14.058593805);
                k++;
            };
            m.RenderToFile("1.mp4");
        }
    }
}
