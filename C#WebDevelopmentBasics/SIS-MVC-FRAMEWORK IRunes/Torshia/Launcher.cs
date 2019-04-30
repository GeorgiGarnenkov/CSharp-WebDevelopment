using SIS.Framework;

namespace Torshia
{
    public class Launcher
    {
        public static void Main()
        {
            WebHost.Start(new StartUp());
        }
    }
}