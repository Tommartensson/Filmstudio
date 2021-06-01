using Filmstudion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> Get();
        Task<Movie> GetById(int id);
        Task<Movie> Create(Movie movie);
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
