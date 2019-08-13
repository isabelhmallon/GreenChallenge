using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenChallenge.Models
{
    public class Day
    {
        [Key]
        public int dayNumber { get; set; }

        [Key]
        public int userId { get; set; }
        public IEnumerable<UserTaskLog> tasksCompleted { get; set; }
        public Boolean dayCompleted { get; set; }
    }
}