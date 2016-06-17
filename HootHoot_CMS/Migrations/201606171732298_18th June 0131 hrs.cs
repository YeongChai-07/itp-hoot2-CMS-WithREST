namespace HootHoot_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18thJune0131hrs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "answering_duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "answering_duration");
        }
    }
}
