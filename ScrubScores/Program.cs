using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrubScores
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Usage();
                Console.ReadLine();
                return;
            }
            var success = new Scrubber().ScrubDirectoryPath(args[0]);
            if (!success) Console.ReadLine();
        }

        static void Usage()
        {
            Console.WriteLine("ScrubScores.exe <DirPathToLogFiles>");
            Console.WriteLine(@"\t example: ScrubScores.exe c:\dir\dir\Logs");
        }
    }
}
