namespace HootHoot_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201606141429hrs_AmendTableColumnName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FunFacts", "stationID", "dbo.Stations");
            DropForeignKey("dbo.Questions", "stationID", "dbo.Stations");

            DropPrimaryKey("dbo.FunFacts", new[] { "funfactID" });
            DropPrimaryKey("dbo.Questions", new[] { "questionID" });
            DropPrimaryKey("dbo.Stations", new[] { "stationID" });

            RenameColumn(table: "dbo.FunFacts", name: "funfactID", newName: "funfact_id");
            RenameColumn(table: "dbo.FunFacts", name: "stationID", newName: "station_id");
            RenameColumn(table: "dbo.Questions", name: "questionID", newName: "question_id");
            RenameColumn(table: "dbo.Questions", name: "stationID", newName: "station_id");
            RenameColumn(table: "dbo.Stations", name: "stationID", newName: "station_id");
            
            RenameIndex(table: "dbo.FunFacts", name: "IX_stationID", newName: "IX_station_id");
            RenameIndex(table: "dbo.Questions", name: "IX_stationID", newName: "IX_station_id");

            //AddColumn("dbo.FunFacts", "funfact_id", c => c.Int(nullable: false, identity: true));
            //AddColumn("dbo.Stations", "station_id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Stations", "station_name", c => c.String());
            //AddColumn("dbo.Questions", "question_id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Questions", "question_name", c => c.String());
            AddColumn("dbo.Questions", "question_type", c => c.String());
            AddColumn("dbo.Questions", "option_type", c => c.String());
            AddColumn("dbo.Questions", "option_1", c => c.String());
            AddColumn("dbo.Questions", "option_2", c => c.String());
            AddColumn("dbo.Questions", "option_3", c => c.String());
            AddColumn("dbo.Questions", "option_4", c => c.String());
            AddColumn("dbo.Questions", "correct_option", c => c.String());
            AddPrimaryKey("dbo.FunFacts", "funfact_id");
            AddPrimaryKey("dbo.Stations", "station_id");
            AddPrimaryKey("dbo.Questions", "question_id");
            AddForeignKey("dbo.FunFacts", "station_id", "dbo.Stations", "station_id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "station_id", "dbo.Stations", "station_id", cascadeDelete: true);
            //DropColumn("dbo.FunFacts", "funfactID");
            //DropColumn("dbo.Stations", "stationID");
            DropColumn("dbo.Stations", "stationName");
            //DropColumn("dbo.Questions", "questionID");
            DropColumn("dbo.Questions", "questionName");
            DropColumn("dbo.Questions", "questionType");
            DropColumn("dbo.Questions", "optionType");
            DropColumn("dbo.Questions", "option1");
            DropColumn("dbo.Questions", "option2");
            DropColumn("dbo.Questions", "option3");
            DropColumn("dbo.Questions", "option4");
            DropColumn("dbo.Questions", "correctoption");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "correctoption", c => c.String());
            AddColumn("dbo.Questions", "option4", c => c.String());
            AddColumn("dbo.Questions", "option3", c => c.String());
            AddColumn("dbo.Questions", "option2", c => c.String());
            AddColumn("dbo.Questions", "option1", c => c.String());
            AddColumn("dbo.Questions", "optionType", c => c.String());
            AddColumn("dbo.Questions", "questionType", c => c.String());
            AddColumn("dbo.Questions", "questionName", c => c.String());
            AddColumn("dbo.Questions", "questionID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Stations", "stationName", c => c.String());
            AddColumn("dbo.Stations", "stationID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.FunFacts", "funfactID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Questions", "station_id", "dbo.Stations");
            DropForeignKey("dbo.FunFacts", "station_id", "dbo.Stations");
            DropPrimaryKey("dbo.Questions");
            DropPrimaryKey("dbo.Stations");
            DropPrimaryKey("dbo.FunFacts");
            DropColumn("dbo.Questions", "correct_option");
            DropColumn("dbo.Questions", "option_4");
            DropColumn("dbo.Questions", "option_3");
            DropColumn("dbo.Questions", "option_2");
            DropColumn("dbo.Questions", "option_1");
            DropColumn("dbo.Questions", "option_type");
            DropColumn("dbo.Questions", "question_type");
            DropColumn("dbo.Questions", "question_name");
            DropColumn("dbo.Questions", "question_id");
            DropColumn("dbo.Stations", "station_name");
            DropColumn("dbo.Stations", "station_id");
            DropColumn("dbo.FunFacts", "funfact_id");
            AddPrimaryKey("dbo.Questions", "questionID");
            AddPrimaryKey("dbo.Stations", "stationID");
            AddPrimaryKey("dbo.FunFacts", "funfactID");
            RenameIndex(table: "dbo.Questions", name: "IX_station_id", newName: "IX_stationID");
            RenameIndex(table: "dbo.FunFacts", name: "IX_station_id", newName: "IX_stationID");
            RenameColumn(table: "dbo.Questions", name: "station_id", newName: "stationID");
            RenameColumn(table: "dbo.FunFacts", name: "station_id", newName: "stationID");
            AddForeignKey("dbo.Questions", "stationID", "dbo.Stations", "stationID", cascadeDelete: true);
            AddForeignKey("dbo.FunFacts", "stationID", "dbo.Stations", "stationID", cascadeDelete: true);
        }
    }
}
