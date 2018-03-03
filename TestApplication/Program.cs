using System;
namespace TestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (OpenTK.Toolkit.Init(new OpenTK.ToolkitOptions() { Backend = OpenTK.PlatformBackend.PreferX11 }))
            {
                Window w = new Window();
                w.Run();
                w.Dispose();
            }
        }
    }
}
