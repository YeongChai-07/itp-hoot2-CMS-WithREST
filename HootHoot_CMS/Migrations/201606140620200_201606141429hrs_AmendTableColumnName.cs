namespace HootHoot_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201606141429hrs_AmendTableColumnName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FunFacts", "station_ID", "dbo.Stations");
            DropForeignKey("dbo.Questions", "station_ID", "dbo.Stations");

            DropPrimaryKey("dbo.FunFacts", new[] { "funFact_ID" });
            DropPrimaryKey("dbo.Questions", new[] { "question_ID" });
            DropPrimaryKey("dbo.Stations", new[] { "station_ID" });

            RenameColumn(table: "dbo.FunFacts", name: "funFact_ID", newName: "funfact_id");
            RenameColumn(table: "dbo.FunFacts", name: "station_ID", newName: "station_id");
            RenameColumn(table: "dbo.Questions", name: "question_ID", newName: "question_id");
            RenameColumn(table: "dbo.Questions", name: "station_ID", newName: "station_id");
            RenameColumn(table: "dbo.Stations", name: "station_ID", newName: "station_id");

            RenameIndex(table: "dbo.FunFacts", name: "IX_station_ID", newName: "IX_station_id");
            RenameIndex(table: "dbo.Questions", name: "IX_station_ID", newName: "IX_station_id");
            
            RenameColumn("dbo.Stations", name:"station_Name", newName: "station_name");
            RenameColumn("dbo.Questions", name:"question_Name", newName: "question_name");
            RenameColumn("dbo.Questions", name:"question_Type", newName: "question_type");
            RenameColumn("dbo.Questions", name:"option_Type", newName: "option_type");

            RenameColumn("dbo.FunFacts", name: "funFact_Desc", newName: "funfact");

            AddPrimaryKey("dbo.FunFacts", "funfact_id");
            AddPrimaryKey("dbo.Stations", "station_id");
            AddPrimaryKey("dbo.Questions", "question_id");
            AddForeignKey("dbo.FunFacts", "station_id", "dbo.Stations", "station_id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "station_id", "dbo.Stations", "station_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.FunFacts", "funFact_Desc", c => c.String());
            AddColumn("dbo.Stations", "frameURL", c => c.String());
            DropIndex("dbo.Questions", new[] { "station_id" });
            DropIndex("dbo.FunFacts", new[] { "station_id" });
            DropColumn("dbo.FunFacts", "funfact");
            CreateIndex("dbo.Questions", "station_ID");
            CreateIndex("dbo.FunFacts", "station_ID");
        }
    }
}
