namespace HootHoot_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18thJune0352HRSAddDataAnnotationforViews : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "question_name", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "question_type", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "option_type", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "option_1", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "option_2", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "option_3", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "option_4", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "correct_option", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "correct_option", c => c.String());
            AlterColumn("dbo.Questions", "option_4", c => c.String());
            AlterColumn("dbo.Questions", "option_3", c => c.String());
            AlterColumn("dbo.Questions", "option_2", c => c.String());
            AlterColumn("dbo.Questions", "option_1", c => c.String());
            AlterColumn("dbo.Questions", "option_type", c => c.String());
            AlterColumn("dbo.Questions", "question_type", c => c.String());
            AlterColumn("dbo.Questions", "question_name", c => c.String());
        }
    }
}
