namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "AboutFilm", c => c.String(unicode: true, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "AboutFilm", c => c.String(nullable: false, unicode: true, storeType: "text"));
        }
    }
}
