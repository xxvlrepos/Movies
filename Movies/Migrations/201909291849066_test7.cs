namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "Comment", c => c.String(maxLength: 300, unicode: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "Comment", c => c.String(unicode: false, storeType: "text"));
        }
    }
}
