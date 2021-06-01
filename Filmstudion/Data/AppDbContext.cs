﻿using Filmstudion.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

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

        }
    }
}