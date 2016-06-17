namespace HootHoot_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18thJUNE0723HRSAddFOREIGNKEYStoQuestionsmodelfromtheOptionTypeandQuestionTypemodel : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Questions", "question_type", "dbo.QuestionTypes", "questiontype_id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "option_type", "dbo.OptionTypes", "optiontype_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "question_type", "dbo.QuestionTypes");
            DropForeignKey("dbo.Questions", "option_type", "dbo.OptionTypes");
        }
    }
}
