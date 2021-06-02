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
    public class MovieCoRepository : IMovieCoRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MovieCoRepository> _logger;
        public MovieCoRepository(AppDbContext context, ILogger<MovieCoRepository> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<MovieCo> Create(MovieCo movieCo)
        {
            _context.MovieCos.Add(movieCo);
            await _context.SaveChangesAsync();
            return movieCo;
        }
        public async Task<IEnumerable<MovieCo>> Get()
        {
            return await _context.MovieCos.ToListAsync();
        }
        public async Task<MovieCo> GetById(int id)
        {
            IQueryable<MovieCo> query = _context.MovieCos;

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
