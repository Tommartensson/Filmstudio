using Filmstudion.Data;
using Filmstudion.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MovieRepository> _logger;
        public MovieRepository(AppDbContext context, ILogger<MovieRepository> logger)
        {
            _logger = logger;
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
            IQueryable<Movie> query = _context.Movies;

            query = query.Where(c => c.id == id);

            return await query.FirstOrDefaultAsync();
        }
         
        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
