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
        public Boolean complete { get; set; }

        public int dayNumber {get; set;}
        
        public Day day { get; set; }

   
        public int challengeTaskId { get; set; }
        
        public ChallengeTask ChallengeTask { get; set; }
    }
}