﻿using System;
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
        public static void W1044480H128()
        {
            var m = new Mp4Render(9, 5, 256, 60, 1920, 1080);
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
    }
}
