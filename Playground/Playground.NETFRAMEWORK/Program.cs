using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Playground.NETFRAMEWORK.Models;
using Playground.NETFRAMEWORK.Tests;
using Timer = System.Timers.Timer;

// ReSharper disable LocalizableElement

namespace Playground.NETFRAMEWORK
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Starting....");
            Console.WriteLine("--------------------------------------------------------------------");
            Proccesses.ProccessStatus();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Hit any key to Continue!");
            Console.ReadKey();
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
            IQueryable<Blog> query = db.Blogs.Where(tt => tt.Id > 100).OrderByDescending(tt => tt.Id);
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
