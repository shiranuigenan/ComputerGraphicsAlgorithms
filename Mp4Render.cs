using System.Diagnostics;

namespace ComputerGraphicsAlgorithms
{
    public delegate void FrameRenderEvent(int framNumber, byte[] frameBuffer);
    public class Mp4Render
    {
        public int Width;
        public int Height;
        public int FrameCount;
        public int Fps;

        public Mp4Render(int width, int height, int frameCount = 1, int fps = 20)
        {
            Width = width;
            Height = height;
            FrameCount = frameCount;
            Fps = fps;
        }
        public event FrameRenderEvent OnFrameRender;
        public void RenderToFile(string filePath)
        {
            var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v {Width}x{Height} -r {Fps} -i -";
            var outputArgs = $"-c:v libx265 -preset ultrafast -crf 0 {filePath}";
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

            var framBuffer = new byte[Width * Height * 6];
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
