using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Playground.ASPNETCORE.Models
{
    public class Profile
    {
        //[Display(ResourceType = typeof(Translations.Labels), Name = "FirstName")]
        public string FirstName { get; set; }

        //[Display(ResourceType = typeof(Translations.Labels), Name = "LastName")]
        public string LastName { get; set; }
    }
}
