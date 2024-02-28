using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class User_contact
    {
        public int user_contect_id;

        public int secondUserAcceptedRequest { get; set; }
        public User firstUser { get; set; }
        public User secondUser { get; set; }
    }
    enum requestStatus
    {
        pending=0, accepted=1, rejected=2, suspended=3
    }
}
