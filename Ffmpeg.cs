using System.Diagnostics;

namespace ComputerGraphicsAlgorithms;

public static class Ffmpeg
{
    public static Process Start(string inputArgs, string outputArgs)
    {
        var process = new Process
        {
            StartInfo =
            {
                FileName = "ffmpeg.exe",
                Arguments = $"{inputArgs} {outputArgs}",
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
        };

        process.Start();

        return process;
    }

    public static void Finish(Process process)
    {
        process.StandardInput.BaseStream.Close();
        process.WaitForExit();
    }
}
