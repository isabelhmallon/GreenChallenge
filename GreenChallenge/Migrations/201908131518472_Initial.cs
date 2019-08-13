namespace GreenChallenge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dayNumber = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                        dayCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserChallenges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserTaskLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        dateCompleted = c.DateTime(nullable: false),
                        complete = c.Boolean(nullable: false),
                        dayNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserTaskLogs");
            DropTable("dbo.UserChallenges");
            DropTable("dbo.Days");
        }
    }
}
