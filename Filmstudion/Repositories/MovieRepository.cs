using Filmstudion.Data;
using Filmstudion.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> Create (Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
        public async Task<IEnumerable<Movie>> Get()
        {
            return await _context.Movies.ToListAsync();
        }
        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.FindAsync(id);
        }
        
        public async Task Update(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var FindMovie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(FindMovie);
            await _context.SaveChangesAsync();
        }

    }
}
