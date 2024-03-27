using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class Artist
    {
        public int Id { get; set; }
        public string name { get; set; }
        public ICollection<Song> songs { get; set; }
    }
}
