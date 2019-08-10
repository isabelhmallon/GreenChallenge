using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenChallenge.Models
{
    public class Challenge
    {
        int id;
        String name;
        String description;
        IEnumerable<Task> tasks;
    }
}