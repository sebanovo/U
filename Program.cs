using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using U.src;

namespace U;
class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var nativeWindowSettings = new NativeWindowSettings()
        {
            ClientSize = new Vector2i(800, 600),
            Title = "U"
        };
        using App  window = new(GameWindowSettings.Default, nativeWindowSettings);
        window.Run();
    }
}
