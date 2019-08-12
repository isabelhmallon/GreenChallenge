namespace GreenChallenge.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GreenChallenge.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<GreenChallenge.Models.GreenChallengeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GreenChallenge.Models.GreenChallengeContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.ChallengeTasks.AddOrUpdate(p => p.id,
            new ChallengeTask { name = "test", description = "test" }
            
            );


        }
    }
}
