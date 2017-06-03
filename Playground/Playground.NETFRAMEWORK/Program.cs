using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Playground.NETFRAMEWORK.Models;

namespace Playground.NETFRAMEWORK
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var recursive = new Recursive();
            var items = recursive.StartRecursive();
            Console.WriteLine(items.Count + " counted!");
            //  CreateDummyData();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Hit any key to Continue!");
            Console.ReadKey();
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
                   LocationName = "Test "+next
                });
            }
            db.Locations.AddRange(list);
            db.SaveChanges();
            for (int i = 0; i < list.Count; i++)
            {
                var next = rand.Next(0,list.Count-1);
                list[i].ParentLocationID = list[next].LocationID;
                db.Entry(list[i]).State = EntityState.Modified;
            }
            db.SaveChanges();
        }
    }
}
