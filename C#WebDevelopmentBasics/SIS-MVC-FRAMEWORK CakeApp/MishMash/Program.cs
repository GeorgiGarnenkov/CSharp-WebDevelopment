using System;
using SIS.MvcFramework;

namespace MishMash
{
    class Program
    {
        static void Main()
        {
            WebHost.Start(new StartUp());
        }
    }
}
