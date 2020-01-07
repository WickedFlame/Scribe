using System;
using System.Diagnostics;

namespace Scribe.Integration.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            new LoggerConfiguration()
                .AddWriter(new ConsoleLogWriter())
                .BuildLogger()
                .SetTraceListener();

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            Console.ReadLine();
        }
    }
}
