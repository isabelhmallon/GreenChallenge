using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenChallenge.Models
{
    public class UserChallenge
    {
        public int Id { get; set; }

        [Required]
        public int userID { get; set; }
        public IEnumerable<Day> day { get; set; }

        [Required]
        public IEnumerable<Challenge> challenge { get; set; }
    }
}