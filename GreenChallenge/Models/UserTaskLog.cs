using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenChallenge.Models
{
    public class UserTaskLog
    {
        public int userId { get; set; }
        public DateTime dateCompleted { get; set; }
        public Boolean complete { get; set; }
        public int dayNumber {get; set;}
        public IEnumerable<ChallengeTask> tasks { get; set; }
    }
}