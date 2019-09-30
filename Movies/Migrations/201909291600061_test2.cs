namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Genres", "GenreName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Genres", "GenreName", c => c.String(nullable: false, maxLength: 50, unicode: true));
            AlterColumn("dbo.Films", "Name", c => c.String(nullable: false, maxLength: 50, unicode: true));
        }
    }
}
