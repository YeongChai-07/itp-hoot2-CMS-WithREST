namespace HootHoot_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201606141429hrs_AmendTableColumnName : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.FunFacts", "station_ID", "dbo.Stations");
            //DropForeignKey("dbo.Questions", "station_ID", "dbo.Stations");

            DropPrimaryKey("dbo.FunFacts", new[] { "funfactID" });
            DropPrimaryKey("dbo.Questions", new[] { "questionID" });
            //DropPrimaryKey("dbo.Stations", new[] { "station_ID" });

            RenameColumn(table: "dbo.FunFacts", name: "funfactID", newName: "funfact_id");
            //RenameColumn(table: "dbo.FunFacts", name: "station_ID", newName: "station_id");

            //RENAME the Stations table column names
            RenameColumn(table:"dbo.Stations", name: "stationName", newName: "station_name");

            //RENAME the Questions table column names
            RenameColumn(table:"dbo.Questions", name: "questionID", newName: "question_id");
            RenameColumn(table: "dbo.Questions", name: "questionName", newName: "question_name");
            RenameColumn(table: "dbo.Questions", name: "questionType", newName: "question_type");
            RenameColumn(table: "dbo.Questions", name: "optionType", newName: "option_type");
            RenameColumn(table: "dbo.Questions", name: "option1", newName: "option_1");
            RenameColumn(table: "dbo.Questions", name: "option2", newName: "option_2");
            RenameColumn(table: "dbo.Questions", name: "option3", newName: "option_3");
            RenameColumn(table: "dbo.Questions", name: "option4", newName: "option_4");
            RenameColumn(table: "dbo.Questions", name: "correctoption", newName: "correct_option");
            //RenameColumn(table: "dbo.Questions", name: "stationID", newName: "station_id");
            // RenameColumn(table: "dbo.Stations", name: "station_ID", newName: "station_id");

            //RenameIndex(table: "dbo.FunFacts", name: "IX_station_ID", newName: "IX_station_id");
            //RenameIndex(table: "dbo.Questions", name: "IX_station_ID", newName: "IX_station_id");

            //RenameColumn("dbo.FunFacts", name: "funFact_Desc", newName: "funfact");

            AddPrimaryKey("dbo.FunFacts", "funfact_id");
            //AddPrimaryKey("dbo.Stations", "station_id");
            AddPrimaryKey("dbo.Questions", "question_id");
            //AddForeignKey("dbo.FunFacts", "station_id", "dbo.Stations", "station_id", cascadeDelete: true);
            //AddForeignKey("dbo.Questions", "station_id", "dbo.Stations", "station_id", cascadeDelete: true);
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
