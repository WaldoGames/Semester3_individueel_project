using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class ShowDto
    {
        public int Id { get; set; }
        public string show_name { get; set; }
        public string show_description { get; set; }
        public string show_language { get; set; }
    }
}
