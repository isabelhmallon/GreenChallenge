using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace GreenChallenge.Models
{
    public class GreenChallengeContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public GreenChallengeContext() : base("name=GreenChallengeContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<GreenChallenge.Models.ChallengeTask> ChallengeTasks { get; set; }

        public System.Data.Entity.DbSet<GreenChallenge.Models.Challenge> Challenges { get; set; }

        public System.Data.Entity.DbSet<GreenChallenge.Models.UserChallenge> UserChallenges { get; set; }

        public System.Data.Entity.DbSet<GreenChallenge.Models.UserTaskLog> UserTaskLogs { get; set; }

        public System.Data.Entity.DbSet<GreenChallenge.Models.Day> Days { get; set; }
    }
}
