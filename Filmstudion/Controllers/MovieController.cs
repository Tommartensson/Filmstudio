using AutoMapper;
using Filmstudion.Models;
using Filmstudion.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

            public MovieController(IMovieRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        // Api/Movie Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {

            try
            {
                var results = _repository.Get();
                MovieModel[] models = _mapper.Map<MovieModel[]>(results);
                return Ok(models);
            }
            catch
            {
                return BadRequest("DataBase Failure");
            }
        }

        // Api/Movie/{id} Get
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // Api/Movie Post
        [HttpPost]
        public void Post([FromBody] string movie)
        {
        }

        // Api/Movie/{id} Put/Patch
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string movie)
        {
        }

        // // Api/Movie/{id} Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
