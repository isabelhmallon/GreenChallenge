using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenChallenge.Models
{
    public class ChallengeTask
    {
        public int id { get; set; }
        public String name { get; set; }
        public String description { get; set; }       
        public int challengeId { get; set; }
    }
}