using NvAPIWrapper;
using NvAPIWrapper.Native;
using NvAPIWrapper.Display;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native.Display.Structures;

namespace Kakki;

internal class Vibrance
{
    private static DisplayHandle _displayHandle;
    private static int _oldVibrance;
    private static readonly int _newVibrance = 100;
    private static bool _vibranceChanged;
    
    internal static void Initialise()
    {
        var model = PhysicalGPU.GetPhysicalGPUs()[0];
        var driver = NVIDIA.DriverVersion.ToString().Insert(3, ".");
        _displayHandle = Display.GetDisplays()[0].Handle;
        _oldVibrance = DisplayApi.GetDVCInfoEx(_displayHandle).CurrentLevel;
        
        Console.WriteLine(
            "GPU model: {0}\n" +
            "Driver version: {1}\n" +
            "Current vibrance level: {2}\n",
            model, driver, _oldVibrance);
    }

    internal static void SetVibrance(bool focus)
    {
        try
        {
            switch (focus)
            {
                case true when !_vibranceChanged:
                    Console.WriteLine("Game in focus! Setting vibrance to {0}", _newVibrance);
                    DisplayApi.SetDVCLevelEx(_displayHandle, _newVibrance);
                    _vibranceChanged = true;
                    return;
                case false when _vibranceChanged:
                    Console.WriteLine("No listed process in focus. Setting vibrance to {0}", _oldVibrance);
                    DisplayApi.SetDVCLevelEx(_displayHandle, _oldVibrance);
                    _vibranceChanged = false;
                    break;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine("An error has occured. Error message: {0}", error);
        }
    }
}