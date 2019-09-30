namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "AboutFilm", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "AboutFilm", c => c.String(maxLength: 1000, unicode: false));
        }
    }
}
