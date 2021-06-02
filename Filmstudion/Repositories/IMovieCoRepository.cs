using Filmstudion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Repositories
{
    public interface IMovieCoRepository
    {
        Task<IEnumerable<MovieCo>> Get();
        Task<MovieCo> GetById(int id);
        Task<MovieCo> Create(MovieCo movieCo);
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
