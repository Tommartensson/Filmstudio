using AutoMapper;
using Filmstudion.Entities;
using Filmstudion.Models;
using Filmstudion.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Filmstudion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCoController : ControllerBase
    {
        private readonly IMovieCoRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _Link;
       
 

        public MovieCoController(IMovieCoRepository repository,
            IMapper mapper,
            LinkGenerator link
            )
        {
            _Link = link;
            _mapper = mapper;
            _repository = repository;
            
        }
        // Api/MovieCo Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieCo>>> Get()
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
        public async Task<ActionResult<MovieCoModel>> Post([FromBody] MovieCoModel model)
        {
            try
            {
                var location = _Link.GetPathByAction("Get", "MovieCo", new { name =  model.name});
                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Couldnt use current name");
                }
                var movieCo = _mapper.Map<MovieCo>(model);
                await _repository.Create(movieCo);
                return Created("", _mapper.Map<MovieCoModel>(movieCo));
            }
            catch
            {
                return BadRequest("DataBase Failure");
            }
        }

        // Api/Movie/{id} Put/Patch
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieCoModel>> Put(int id, [FromBody] MovieCoModel movie)
        {
            try
            {
                var oldMovieCo = await _repository.GetById(id);
                if (oldMovieCo == null) NotFound("Couldnt not find");


                _mapper.Map(movie, oldMovieCo);
                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<MovieCoModel>(oldMovieCo);
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

            if (await _repository.SaveChangesAsync())
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
