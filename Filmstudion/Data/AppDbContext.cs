using Filmstudion.Entities;
using Filmstudion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Data
{
    public class AppDbContext : IdentityDbContext<Admin>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCo> MovieCos { get; set; }
        public DbSet<Admin> Admin { get; set; }
   

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>().ToTable("Movies");
            builder.Entity<Movie>().HasKey(p => p.id);
            builder.Entity<Movie>().Property(p => p.id).IsRequired();
            builder.Entity<Movie>().Property(p => p.Borrowed).IsRequired();
            builder.Entity<Movie>().Property(p => p.name);
            builder.Entity<Movie>().Property(p => p.releaseYear);
            builder.Entity<Movie>().Property(p => p.country);
            builder.Entity<Movie>().Property(p => p.director);

            builder.Entity<Movie>().HasData
                (
                new Movie { id = 1, name = "LoR", country = "Sweden", director = "blank", releaseYear = 1998, Borrowed = false },
                new Movie { id = 2, name = "Transformers", country = "Sweden", director = "blank", releaseYear = 2000, Borrowed = false },
                new Movie { id = 3, name = "Ted", country = "Sweden", director = "blank", releaseYear = 1472, Borrowed = false },
                new Movie { id = 4, name = "Backstage", country = "Sweden", director = "blank", releaseYear = 1990, Borrowed = true },
                new Movie { id = 5, name = "TMNT", country = "Sweden", director = "blank", releaseYear = 1991, Borrowed = false },
                new Movie { id = 6, name = "Star-wars", country = "Sweden", director = "blank", releaseYear = 1992, Borrowed = true }

            );
            builder.Entity<MovieCo>().HasData(
                new MovieCo
                {
                    id = 1,
                    name = "Universal",
                    Password = "Un1versal!",
                    Number = 0767769447,
                    nameOfCo = "Foo Bar",
                    Email = "Foo.bar@gmail.com",
                    place = "USA"

                });
            builder.Entity<MovieCo>().HasData(
                new MovieCo
                {
                    id = 2,
                    name = "Dreamworks",
                    Password = "Dr3amW0rks!",
                    Number = 0767723423,
                    nameOfCo = "John Denver",
                    Email = "John.Denver@gmail.com",
                    place = "Colombia"

                });

            builder.Entity<IdentityRole>().HasData(
                new Admin
                {
                    UserName = "Snabel",
                    password = "P@ssW0rd!",
                    Email = "Snabel.Snabelsson@gmail.com",
                    

                });
          

           

        }
    }
}
