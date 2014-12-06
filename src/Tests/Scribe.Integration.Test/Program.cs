using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe.Integration.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new ScribeBootstrapper();
            bootstrapper.Initialize();

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            Console.ReadLine();
        }
    }
}
