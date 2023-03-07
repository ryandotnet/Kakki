using NvAPIWrapper;
using NvAPIWrapper.Native;
using NvAPIWrapper.Display;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Mosaic;

namespace Kakki;

internal class Vibrance
{
    internal static void GetVibrance()
    {
        var model = NVIDIA.ChipsetInfo.ChipsetName;
        var display = Display.GetDisplays();
        Console.WriteLine("Number of Displays: {0}", display.Length);
        var handle = display[0].Handle;
        var vibrance = NvAPIWrapper.Native.DisplayApi.GetDVCInfo(handle);

        Console.WriteLine("GPU model: {0}", model);
    }
}