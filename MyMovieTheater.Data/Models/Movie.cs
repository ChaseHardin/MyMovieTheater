using System;
using System.Data.Entity.ModelConfiguration;

namespace MyMovieTheater.Data.Models
{
    public class Movie
    {
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public DateTime ShowTime { get; set; }
        public decimal TicketPrice { get; set; }
        public string Rating { get; set; }
    }

    internal class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            ToTable("dbo.Movies");
            HasKey(x => x.MovieId);

            Property(x => x.MovieId).HasColumnName("MovieId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired();
            Property(x => x.ShowTime).HasColumnName("ShowTime").IsRequired();
            Property(x => x.TicketPrice).HasColumnName("TicketPrice").IsRequired();
            Property(x => x.Rating).HasColumnName("Rating").IsRequired();
        }
    }
}