namespace HootHoot_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18thJUN0656HRSAddOptionTypeQuestionTypemodelReferencedtoQuestionsmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OptionTypes",
                c => new
                    {
                        optiontype_id = c.Int(nullable: false, identity: true),
                        optiontype = c.String(),
                    })
                .PrimaryKey(t => t.optiontype_id);
            
            CreateTable(
                "dbo.QuestionTypes",
                c => new
                    {
                        questiontype_id = c.Int(nullable: false, identity: true),
                        questiontype = c.String(),
                    })
                .PrimaryKey(t => t.questiontype_id);
            
            AlterColumn("dbo.Questions", "question_type", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "option_type", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "question_type");
            CreateIndex("dbo.Questions", "option_type");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Questions", new[] { "option_type" });
            DropIndex("dbo.Questions", new[] { "question_type" });
            AlterColumn("dbo.Questions", "option_type", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "question_type", c => c.String(nullable: false));
            DropTable("dbo.QuestionTypes");
            DropTable("dbo.OptionTypes");
        }
    }
}
