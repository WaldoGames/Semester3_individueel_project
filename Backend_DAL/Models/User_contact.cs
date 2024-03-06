using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class User_contact
    {
        [Key]
        public int Id { get; set; }

        public requestStatus secondUserAcceptedRequest { get; set; }
        public int firstUserId { get; set; }

        // Navigation property
        [ForeignKey("firstUserId")]
        public User firstUser { get; set; }

        public int secondUserId { get; set; }

        // Navigation property
        [ForeignKey("secondUserId")]
        
        public User secondUser { get; set; }
    }
    enum requestStatus
    {
        pending=0, accepted=1, rejected=2, suspended=3
    }
}
