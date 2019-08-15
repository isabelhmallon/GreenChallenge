using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GreenChallenge.Models
{
    public class Day
    {
        public int id { get; set; }
        public int dayNumber { get; set; }        
        public int username { get; set; }
        public ICollection<UserTaskLog> tasksCompleted { get; set; }
        public Boolean dayCompleted { get; set; }
    }
}