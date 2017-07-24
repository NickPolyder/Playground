using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Playground.NETCORE
{
    class Program
    {
        static void Main(string[] args)
        {

            //var items = GetCurrentDirAndFile();
            //Console.WriteLine($"Directory {items.dir.Name} File {items.file?.Name}");
            Task.Run(async()=>await CallHttpClient());
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

        public static async Task CallHttpClient()
        {
            Console.WriteLine("Start The Request");
            using (var http = new HttpClient())
            {
                var get = await http.GetAsync(new Uri("http://heroes.local.port:15555"));
                var content = await get.Content.ReadAsStringAsync();
                Console.WriteLine(get.StatusCode);

            }
        }
    }
}