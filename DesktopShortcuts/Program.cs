using System;
using System.Linq;

namespace DesktopShortcuts
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                args = new[] { "/?" };

            try
            {
                Run(args);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.ExitCode = 1;
            }

        }

        static void Run(string[] args)
        {
            var state = new DesktopState();
            switch (args[0].ToLower())
            {
                case "/record"  : state.Record();           break;
                case "/restore" : state.Restore();          break;
                case "/discard" : state.Discard();          break;
                case "/output"  : Console.WriteLine(state); break;
                default         : OutputHelp();             break;
            }
        }

        static void OutputHelp()
        {
            Console.WriteLine("DesktopShortcuts");
            Console.WriteLine("Options:");
            Console.WriteLine("    /record");
            Console.WriteLine("    /restore");
            Console.WriteLine("    /discard");
            Console.WriteLine("    /output");
        }
    }
}