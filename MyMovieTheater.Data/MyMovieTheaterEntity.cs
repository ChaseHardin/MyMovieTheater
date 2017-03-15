using System.Data.Entity;
using MyMovieTheater.Data.Models;

namespace MyMovieTheater.Data
{
    public class MyMovieTheaterEntity : DbContext
    {
        public MyMovieTheaterEntity() : base("MyMovieTheaterConnectionString") { }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MyMovieTheaterEntity>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}