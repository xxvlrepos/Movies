namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test71 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "Comment", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "Comment", c => c.String(maxLength: 300, unicode: false));
        }
    }
}
