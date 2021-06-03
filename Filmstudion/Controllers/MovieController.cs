using AutoMapper;
using Filmstudion.Entities;
using Filmstudion.Models;
using Filmstudion.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _Link;

            public MovieController(IMovieRepository repository, IMapper mapper, LinkGenerator link)
        {
            _Link = link;
            _mapper = mapper;
            _repository = repository;
        }
        // Api/Movie Get
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {

           try
            { 
                var results = _repository.Get();

            


                return Ok(results);
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
            try
            {
                var result = _repository.GetById(id);

                if (result == null) return NotFound();
                return Ok(result);
            }
            catch
            {
                return BadRequest("DataBase Failure");
            }
           
        }

        // Api/Movie Post
        [HttpPost]
        public async Task<ActionResult<MovieModel>> Post([FromBody] MovieModel model)
        {
            try
            {
                var location = _Link.GetPathByAction("Get", "Movie", new { name = model.name });
                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Couldnt use current name");
                }
                var movie = _mapper.Map<Movie>(model);
                await _repository.Create(movie);
                return Created("", _mapper.Map<MovieModel>(movie));
            }
            catch
            {
                return BadRequest("DataBase Failure");
            }
        }

        // Api/Movie/{id} Put/Patch
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieModel>> Put(int id, [FromBody] MovieModel movie)
        {
            try
            {
                var oldMovie = await _repository.GetById(id);
                if (oldMovie == null) NotFound("Couldnt not find");


                _mapper.Map(movie, oldMovie);
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<MovieModel>(oldMovie);
                }
            }
            catch
            {
                return BadRequest("DataBase Failure");
            }
            return BadRequest();
        }

        // // Api/Movie/{id} Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //try
            //{
                var Movie = _repository.GetById(id);
                if (Movie == null) return NotFound();

                _repository.Delete(Movie);

                if(await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                

            /*}
            catch
            {
                return BadRequest("DataBase Failure");
            }*/
            return BadRequest("Fungerade inte");
        }

       
    }
}
