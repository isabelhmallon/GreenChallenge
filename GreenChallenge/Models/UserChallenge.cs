using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GreenChallenge.Models
{
    public class UserChallenge
    {
        public int id { get; set; }
        public string username { get; set; }

        [ForeignKey("userChallengeId")]
        public ICollection<Day> days { get; set; }
        public Boolean challengeCompleted { get; set; }
        public int challengeId { get; set; }       
        public Challenge challenge { get; set; }


    }
}