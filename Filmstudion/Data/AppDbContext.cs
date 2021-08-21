using Filmstudion.Entities;
using Filmstudion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Data
{
    public class AppDbContext : IdentityDbContext<AdminModel>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCo> MovieCos { get; set; }
        public DbSet<MovieModel> MovieModel { get; set; }
       
   

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            

            builder.Entity<Movie>().ToTable("Movies");
            builder.Entity<Movie>().HasKey(p => p.MovieId);
            builder.Entity<Movie>().Property(p => p.MovieId).IsRequired();
            builder.Entity<Movie>().Property(p => p.Loanable);
            builder.Entity<Movie>().Property(p => p.name);
            builder.Entity<Movie>().Property(p => p.releaseYear);
            builder.Entity<Movie>().Property(p => p.country);
            builder.Entity<Movie>().Property(p => p.director);

           

            builder.Entity<Movie>().HasData
                (
                new Movie { MovieId = 1, name = "LoR", country = "Sweden", director = "blank", releaseYear = 1998, Loanable = 3 },
                new Movie { MovieId = 2, name = "Transformers", country = "Sweden", director = "blank", releaseYear = 2000, Loanable = 3 },
                new Movie { MovieId = 3, name = "Ted", country = "Sweden", director = "blank", releaseYear = 1472, Loanable = 3 },
                new Movie { MovieId = 4, name = "Backstage", country = "Sweden", director = "blank", releaseYear = 1990, Loanable = 3 },
                new Movie { MovieId = 5, name = "TMNT", country = "Sweden", director = "blank", releaseYear = 1991, Loanable = 3 },
                new Movie { MovieId = 6, name = "Star-wars", country = "Sweden", director = "blank", releaseYear = 1992, Loanable = 9}

            );
            builder.Entity<MovieCo>().HasData
            (
                new MovieCo
                {
                    MovieCoid = 1,
                    name = "Universal",
                    Password = "Un1versal!",
                    Number = 0767769447,
                    nameOfCo = "Foo Bar",
                    mail = "Foo.bar@gmail.com",
                    place = "USA",
                    MyMovies = new List<Movie>
                    {

                    }
                },
            new MovieCo
            {
                MovieCoid = 2,
                name = "Dreamworks",
                Password = "Dr3amW0rks!",
                Number = 0767723423,
                nameOfCo = "John Denver",
                mail = "John.Denver@gmail.com",
                place = "Colombia",

            }) ; ;



        }
    }
}
