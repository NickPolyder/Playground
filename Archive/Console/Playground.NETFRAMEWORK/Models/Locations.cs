using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.NETFRAMEWORK.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        public int? ParentLocationID { get; set; }

        public int LocationTypeID { get; set; }

        public DateTime? InstallDate { get; set; }

        public string AltPhone { get; set; }

        public string OfficePhone { get; set; }

        public int? PrimaryAddressID { get; set; }
        
        public string LocationName { get; set; }

        public string LocationName2 { get; set; }
        
    }
}
