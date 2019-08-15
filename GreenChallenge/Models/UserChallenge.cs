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
        public int username { get; set; }
        public ICollection<Day> days { get; set; }
        public Boolean challengeCompleted { get; set; }

        public int challengeId { get; set; }
        [ForeignKey("challengeId")]
        public Challenge challenge { get; set; }


    }
}