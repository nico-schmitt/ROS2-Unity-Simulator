using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public enum Tracks
{
    autocross,
    trackdrive,
    skidpad,
    accel
}

public class SimulationManager : MonoBehaviour
{
    private const string dvDockerContainer = "gitlab.dynamics-regensburg.de:5050/dynamics/driverless/rp-driverless";
    private string containerName; // should be thirsty_shaw

    private string _containerPSLine_stdout, _containerPSLine_stderr;
    private string _asmPID_stdout, _asmPID_stderr;
    private string _simPID_stdout, _simPID_stderr;

    [SerializeField] private Tracks _currentTrack;

    public bool IsPlaying {get; private set;}



    // Get docker container name to start the asm and simulator executable on it, both running in the background (-d flag)
    public void StartSimulator()
    {
        IsPlaying = true;

        SimulationProcessHelper.SendCommand($"docker ps | grep {dvDockerContainer}", out _containerPSLine_stdout, out _containerPSLine_stderr, "ContainerInfoLine");
        containerName = SimulationProcessHelper.GetDockerContainerName(_containerPSLine_stdout);

        SimulationProcessHelper.SendCommand($"docker exec -d {containerName} /bin/bash -c " +
            "\". /workspaces/rp-driverless/install/setup.bash && " +
            ". /workspaces/rp-driverless/install/local_setup.bash && " +
            "ros2 run autonomous_system_management asm\"", "StartROS2_ASM");

        
        SimulationProcessHelper.SendCommand($"docker exec -d {containerName} /bin/bash -c " +
            "\". /workspaces/rp-driverless/install/setup.bash && " +
            ". /workspaces/rp-driverless/install/local_setup.bash && " +
            $"ros2 run simulator simulator {_currentTrack}\"", "StartSimulator");
    }

    // Grab the asm and simulator process pid; exit the processes and hardreset them
    // (getting an error when stopping is normal since usually there won't be an asm/simulator process to hardreset)
    public void StopSimulator()
    {
        IsPlaying = false;

        SimulationProcessHelper.SendCommand($"docker exec {containerName} /bin/bash -c \"pgrep asm\"", out _asmPID_stdout, out _asmPID_stderr, "GetASM_PID");

        SimulationProcessHelper.SendCommand($"docker exec {containerName} /bin/bash -c \"pkill -SIGINT asm\"", "KillASM");

        SimulationProcessHelper.SendCommand($"docker exec {containerName} /bin/bash -c \"pkill -SIGKILL asm\"", "HardKillASM", false);

        SimulationProcessHelper.SendCommand($"docker exec {containerName} /bin/bash -c \"pgrep simulator\"", out _simPID_stdout, out _simPID_stdout, "GetSIM_PID");

        SimulationProcessHelper.SendCommand($"docker exec {containerName} /bin/bash -c \"pkill -SIGINT simulator\"", "KillSim");

        SimulationProcessHelper.SendCommand($"docker exec {containerName} /bin/bash -c \"pkill -SIGKILL simulator\"", "HardKillSim", false);
    }

    private void LogError(Process processHandle, string processName)
    {
        if(processHandle.ExitCode != 0)
            throw new Exception($"{processName} exited with code: {processHandle.ExitCode}");
    }
}
