using System;
using SIS.MvcFramework;

namespace PANDAWebApp
{
    class Program
    {
        static void Main()
        {
            WebHost.Start(new StartUp());
        }
    }
}
