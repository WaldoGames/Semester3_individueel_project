using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class Show
    {

        public int Id { get; set; }
        public string show_name { get; set; }
        public string show_description { get; set; }
        public string show_language { get; set;}

        public Collection<User> hosts { get; set; }
    }
}
