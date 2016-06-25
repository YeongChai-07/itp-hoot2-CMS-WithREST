namespace HootHoot_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25thJUNE0522HRSAddStationTypetableandStationIDcolumndataformat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FunFacts", "station_id", "dbo.Stations");
            DropForeignKey("dbo.Questions", "station_id", "dbo.Stations");
            DropIndex("dbo.FunFacts", new[] { "station_id" });
            DropIndex("dbo.Questions", new[] { "station_id" });
            DropPrimaryKey("dbo.Stations");
            DropColumn("dbo.Stations", "station_id");

            CreateTable(
                "dbo.StationTypes",
                c => new
                    {
                        stationtype_id = c.String(nullable: false, maxLength: 128),
                        stationtype = c.String(),
                    })
                .PrimaryKey(t => t.stationtype_id);
            
            AddColumn("dbo.Stations", "station_type", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.FunFacts", "station_id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Stations", "station_id", c => c.String(nullable: false, maxLength: 128));

            //AlterColumn("dbo.Stations", "station_id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Questions", "station_id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Stations", "station_id");
            CreateIndex("dbo.FunFacts", "station_id");
            CreateIndex("dbo.Stations", "station_type");
            CreateIndex("dbo.Questions", "station_id");
            AddForeignKey("dbo.Stations", "station_type", "dbo.StationTypes", "stationtype_id");
            AddForeignKey("dbo.FunFacts", "station_id", "dbo.Stations", "station_id");
            AddForeignKey("dbo.Questions", "station_id", "dbo.Stations", "station_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "station_id", "dbo.Stations");
            DropForeignKey("dbo.FunFacts", "station_id", "dbo.Stations");
            DropForeignKey("dbo.Stations", "station_type", "dbo.StationTypes");
            DropIndex("dbo.Questions", new[] { "station_id" });
            DropIndex("dbo.Stations", new[] { "station_type" });
            DropIndex("dbo.FunFacts", new[] { "station_id" });
            DropPrimaryKey("dbo.Stations");
            AlterColumn("dbo.Questions", "station_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Stations", "station_id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.FunFacts", "station_id", c => c.Int(nullable: false));
            DropColumn("dbo.Stations", "station_type");
            DropTable("dbo.StationTypes");
            AddPrimaryKey("dbo.Stations", "station_id");
            CreateIndex("dbo.Questions", "station_id");
            CreateIndex("dbo.FunFacts", "station_id");
            AddForeignKey("dbo.Questions", "station_id", "dbo.Stations", "station_id", cascadeDelete: true);
            AddForeignKey("dbo.FunFacts", "station_id", "dbo.Stations", "station_id", cascadeDelete: true);
        }
    }
}
