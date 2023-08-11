using System.Diagnostics;

namespace ComputerGraphicsAlgorithms
{
    public delegate void FrameRenderEvent(int framNumber, byte[] frameBuffer);
    public class Mp4Render
    {
        public int InWidth, InHeight;
        public int OutWidth, OutHeight;
        public int FrameCount;
        public int Fps;

        public Mp4Render(int inWidth, int inHeight, int frameCount = 1, int fps = 20, int outWidth = 0, int outHeight = 0)
        {
            InWidth = inWidth;
            InHeight = inHeight;
            OutWidth = outWidth == 0 ? InWidth : outWidth;
            OutHeight = outHeight == 0 ? InHeight : outHeight;

            FrameCount = frameCount;
            Fps = fps;
        }
        public event FrameRenderEvent OnFrameRender;
        public void RenderToFile(string filePath)
        {
            var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v {InWidth}x{InHeight} -r {Fps} -i -";
            var outputArgs = $"-c:v libx265 -preset ultrafast -crf 0";

            if (InWidth != OutWidth || InHeight != OutHeight)
                outputArgs += $" -vf scale={OutWidth}:{OutHeight}";

            outputArgs+= $" {filePath}";
            var p = new Process
            {
                StartInfo =
    {
        FileName = "ffmpeg",
        Arguments = $"{inputArgs} {outputArgs}",
        UseShellExecute = false,
        CreateNoWindow = false,
        RedirectStandardInput = true
    }
            };

            p.Start();
            var ffmpegIn = p.StandardInput.BaseStream;

            var framBuffer = new byte[InWidth * InHeight * 6];
            for (int f = 0; f < FrameCount; f++)
            {
                OnFrameRender(f, framBuffer);
                ffmpegIn.Write(framBuffer);
                ffmpegIn.Flush();
            }

            ffmpegIn.Close();
            p.WaitForExit();
        }
    }
}
