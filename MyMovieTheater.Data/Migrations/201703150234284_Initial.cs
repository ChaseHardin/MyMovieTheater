namespace MyMovieTheater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Guid(nullable: false),
                        Name = c.String(),
                        ShowTime = c.DateTime(nullable: false),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.String(),
                    })
                .PrimaryKey(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Movies");
        }
    }
}
