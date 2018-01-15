using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Playground.NETFRAMEWORK.Models;

namespace Playground.NETFRAMEWORK
{
    public class Recursive
    {
        BlogContext db = new BlogContext();
        public int Counter { get; set; } = 0;
        public Recursive()
        {
            db.Database.Log += (str) =>
            {
                System.Diagnostics.Debug.WriteLine(str, "Sql Query: ");
            };
        }

        public List<Location> StartRecursive()
        {
            return GetChild(50).ToList();
        }
        public IEnumerable<Location> GetChild(int id)
        {
            //var locations = db.Locations.Where(x => x.ParentLocationID == id || x.LocationID == id).Union(
            //    db.Locations.Where(x => x.ParentLocationID == id).SelectMany(y => GetChild(y.LocationID)));
            //return locations;
            var locations = db.Locations.Where(x => x.ParentLocationID == id || x.LocationID == id).ToList();
            if (locations.Count == 1) return locations;
            var locationSubset = locations.Where(tt=>tt.LocationID!=id).SelectMany(tt => GetChild(tt.LocationID)).ToList();
            Counter++;
            return locations.Union(locationSubset);
        }
    }
}
