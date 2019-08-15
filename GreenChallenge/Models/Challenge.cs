using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenChallenge.Models
{
    public class Challenge
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<ChallengeTask> tasks { get; set; }
    }
}