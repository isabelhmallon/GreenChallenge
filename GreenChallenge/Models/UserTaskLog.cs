using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GreenChallenge.Models
{
    public class UserTaskLog
    {
        public int id { get; set; }
        public string username { get; set; }
        public DateTime dateCompleted { get; set; }
        public int dayId { get; set; }        
        public int challengeTaskId { get; set; }      
    }
}