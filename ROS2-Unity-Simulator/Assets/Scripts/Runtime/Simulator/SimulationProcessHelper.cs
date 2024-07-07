using System;
using System.Diagnostics;

public static class SimulationProcessHelper
{
    public static void SendCommand(string command, string name, bool logError = true)
    {
        Process processHandle = CreateProcess(command);
        SetupProcess(processHandle);
        LogError(processHandle, name, logError);
    }

    public static void SendCommand(string command, out string stdout, out string stderr, string name)
    {
        Process processHandle = CreateProcess(command);
        ExecuteProcess(processHandle, out stdout, out stderr);
        LogError(processHandle, name);
    }

    private static Process CreateProcess(string command)
    {
        Process process = new Process()
        {
            StartInfo = {
                FileName = "bash",
                ArgumentList = {"-c", command},
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            }
        };
        UnityEngine.Debug.Log($"Executed command: {command}");
        return process;
    }

    private static void ExecuteProcess(Process processHandle, out string stdout, out string stderr)
    {
        processHandle.Start();
        processHandle.WaitForExit();
        stdout = processHandle.StandardOutput.ReadToEnd();
        stderr = processHandle.StandardError.ReadToEnd();
    }

    private static void SetupProcess(Process processHandle)
    {
        processHandle.Start();
        processHandle.WaitForExit();
    }

    private static void LogError(Process processHandle, string processName, bool logError = true)
    {
        if(processHandle.ExitCode != 0 && logError)
            throw new Exception($"{processName} exited with code: {processHandle.ExitCode}");
    }
    
    public static string GetDockerContainerName(string dockerPSLine)
    {
        // Docker container name is always in the last column of docker ps line output
        int containerNameLength = dockerPSLine.Length - dockerPSLine.LastIndexOf(" ") - 2; // -2 because last character is a newline
        UnityEngine.Debug.Log("Name of container: " + dockerPSLine.Substring(dockerPSLine.LastIndexOf(" ")+1, containerNameLength));
        return dockerPSLine.Substring(dockerPSLine.LastIndexOf(" ")+1, containerNameLength);
    }
}

