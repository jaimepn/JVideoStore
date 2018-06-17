namespace JVideoStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Genres", "Name", c => c.String());

            Sql("Insert Into Genres (Name) Values ('Comedy')");
            Sql("Insert Into Genres (Name) Values ('Action')");
            Sql("Insert Into Genres (Name) Values ('Family')");
            Sql("Insert Into Genres (Name) Values ('Romance')");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Genres", "Name", c => c.Int(nullable: false));
        }
    }
}
