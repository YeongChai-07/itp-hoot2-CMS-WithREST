namespace HootHoot_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FunFacts",
                c => new
                {
                    funfactID = c.Int(nullable: false, identity: true),
                    station_id = c.Int(nullable: false),
                    funfact = c.String(),
                })
                .PrimaryKey(t => t.funfactID)
                .ForeignKey("dbo.Stations", t => t.station_id, cascadeDelete: true)
                .Index(t => t.station_id);

            CreateTable(
                "dbo.Stations",
                c => new
                {
                    station_id = c.Int(nullable: false, identity: true),
                    stationName = c.String(),
                })
                .PrimaryKey(t => t.station_id);

            CreateTable(
                "dbo.Questions",
                c => new
                {
                    questionID = c.Int(nullable: false, identity: true),
                    station_id = c.Int(nullable: false),
                    questionName = c.String(),
                    questionType = c.String(),
                    optionType = c.String(),
                    option1 = c.String(),
                    option2 = c.String(),
                    option3 = c.String(),
                    option4 = c.String(),
                    correctoption = c.String(),
                })
                .PrimaryKey(t => t.questionID)
                .ForeignKey("dbo.Stations", t => t.station_id, cascadeDelete: true)
                .Index(t => t.station_id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Questions", "stationID", "dbo.Stations");
            DropForeignKey("dbo.FunFacts", "stationID", "dbo.Stations");
            DropIndex("dbo.Questions", new[] { "stationID" });
            DropIndex("dbo.FunFacts", new[] { "stationID" });
            DropTable("dbo.Questions");
            DropTable("dbo.Stations");
            DropTable("dbo.FunFacts");
        }
    }
}
