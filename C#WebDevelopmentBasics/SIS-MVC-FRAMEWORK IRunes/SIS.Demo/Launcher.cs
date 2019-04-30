using SIS.Framework;

namespace SIS.Demo
{
    public class Launcher
    {
        public static void Main()
        {
            WebHost.Start(new StartUp());
        }
    }
}