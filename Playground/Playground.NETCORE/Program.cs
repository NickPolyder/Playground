using System;
using System.IO;

namespace Playground.NETCORE
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //var items = GetCurrentDirAndFile();
            //Console.WriteLine($"Directory {items.dir.Name} File {items.file?.Name}");

            Console.WriteLine("Hit Any key.");
            Console.ReadKey();
        }

        public static (FileInfo file, DirectoryInfo dir) GetCurrentDirAndFile()
        {
            (FileInfo f, DirectoryInfo d) tuple;
            tuple.d = new DirectoryInfo(AppContext.BaseDirectory);
            tuple.f= null;
            return tuple;
        }
    }
}