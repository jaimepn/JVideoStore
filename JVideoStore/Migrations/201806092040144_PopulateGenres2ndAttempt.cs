namespace JVideoStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres2ndAttempt : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Genres");

            Sql("SET Identity_Insert Genres ON");
            Sql("Insert Into Genres (Id, Name) Values (1, 'Comedy')");
            Sql("Insert Into Genres (Id, Name) Values (2, 'Action')");
            Sql("Insert Into Genres (Id, Name) Values (3, 'Family')");
            Sql("Insert Into Genres (Id, Name) Values (4, 'Romance')");
            Sql("SET Identity_Insert Genres OFF");
        }
        
        public override void Down()
        {
        }
    }
}
