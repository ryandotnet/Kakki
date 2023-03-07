using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Kakki;

internal class Program
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    private static readonly string[] Games = 
    {
        "csgo",
        "RainbowSix",
        "RainbowSix_Vulkan"
    };
    
    private static System.Timers.Timer _processTimer = new()
    {
        Interval = 500,
        AutoReset = true
    };
    
    private static void Main()
    {
        Vibrance.Initialise();
        _processTimer.Elapsed += ProcessTimerEvent;
        _processTimer.Enabled = true;
        Console.ReadLine();
    }

    private static void ProcessTimerEvent(object? sender, EventArgs e)
    {
        var windowHandle = GetForegroundWindow();
        GetWindowThreadProcessId(windowHandle, out var processId);
        var process = Process.GetProcessById(Convert.ToInt32(processId));
        if (Games.Contains(process.ProcessName))
        {
            Vibrance.SetVibrance(true);
            return;
        }
        
        Vibrance.SetVibrance(false);
    }
}