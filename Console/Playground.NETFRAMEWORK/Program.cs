using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Playground.NETFRAMEWORK.Models;
using Playground.NETFRAMEWORK.Tests;
using Timer = System.Timers.Timer;

// ReSharper disable LocalizableElement
namespace Playground.NETFRAMEWORK
{
    class Program
    {
        public const string Heart = "♥";

        public enum Strategies { Default, Something, SomethingElse }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            WebClientTests.Run();
            Console.WriteLine("Hit any key to Continue!");
            Console.ReadKey();
        }

        public static void SetStrategy(Strategies strategy = Strategies.Default)
        {
            switch (strategy)
            {
                case Strategies.Default:
                    break;
                case Strategies.Something:
                    break;
                case Strategies.SomethingElse:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null);
            }
        }
        public static string CreateContent(string str, int repeat)
        {
            if (repeat < 0) throw new ArgumentOutOfRangeException(nameof(repeat), repeat, "Must be above 0");

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < repeat; i++)
            {
                builder.Append(str);
            }

            return builder.ToString();
        }

        #region Tests

        static void DateTimeHourWatch()
        {
            Console.WriteLine($"Started at: {DateTime.Now}");
            Timer timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true };
            timer.Elapsed += (sender, args) =>
            {
                Console.WriteLine("Hellooooooo");
            };
            timer.Start();
            Thread.Sleep(TimeSpan.FromMinutes(1));

            timer.Stop();
        }


        static void Recursive()
        {
            var recursive = new Recursive();
            var items = recursive.StartRecursive();
            Console.WriteLine(items.Count + " counted!");
        }

        static void SqlQueryToString()
        {
            var db = new BlogContext();
            var id = 100;
            IQueryable<Blog> query = db.Blogs.Where(tt => tt.Id > id).OrderByDescending(tt => tt.Id);
            var sqlString = query.ToString();
            Console.WriteLine(sqlString);
        }

        static void CreateDummyData()
        {
            var db = new BlogContext();
            var list = new List<Location>();
            var rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                var next = rand.Next();
                list.Add(new Location
                {
                    LocationName = "Test " + next
                });
            }
            db.Locations.AddRange(list);
            db.SaveChanges();
            for (int i = 0; i < list.Count; i++)
            {
                var next = rand.Next(0, list.Count - 1);
                list[i].ParentLocationID = list[next].LocationID;
                db.Entry(list[i]).State = EntityState.Modified;
            }
            db.SaveChanges();
        }

        static void JsonMapObjects()
        {
            var json = JsonConvert.SerializeObject(new Blog());

            var model = JsonConvert.DeserializeObject<Blog>(json);
        }

        #endregion

    }
}
