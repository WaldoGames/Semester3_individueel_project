using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class NewShowDto
    {
        public string show_name { get; set; }
        public string show_discription { get; set; }
        public string show_language { get; set; }
        public string auth_sub { get; set; }
    }
}
