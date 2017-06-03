using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.NETFRAMEWORK.Models
{
   public class Blog: BaseModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        
    }
}
