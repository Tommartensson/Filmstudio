using AutoMapper;
using Filmstudion.Entities;
using Filmstudion.Models;
using Filmstudion.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MovieCoController : ControllerBase
    {
        private readonly IMovieCoRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _Link;
        private readonly UserManager<AdminModel> _user;
        private readonly SignInManager<AdminModel> _sign;
        private readonly IConfiguration _config;



        public MovieCoController(IMovieCoRepository repository,
            IMapper mapper,
            LinkGenerator link,
            UserManager<AdminModel> user,
            SignInManager<AdminModel> sign,
            IConfiguration config
            )
        {
            _Link = link;
            _mapper = mapper;
            _repository = repository;
            _user = user;
            _sign = sign;
            _config = config;

        }
        // Api/MovieCo Get
        [AllowAnonymous]
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

        // Api/MovieCo/{id} Get
        [AllowAnonymous]
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
        [AllowAnonymous]
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


               AdminModel acc = new AdminModel()
                {
                    UserName = model.name,
                    password = model.password,
                    isAdmin = false
                };
                var create = await _user.CreateAsync(acc, acc.password);
                if (!create.Succeeded)
                {
                    return BadRequest(create);
                }
                return Created("", _mapper.Map<MovieCoModel>(movieCo));
            }
            catch
            {
                return BadRequest(model);
            }
        }

        
        [Route("CreateToken")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] AdminModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _user.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _sign.CheckPasswordSignInAsync(user, model.password, true);

                    if (result.Succeeded)
                    {
                        if (user.isAdmin == false)
                        {
                            var claims = new[]
                            {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                            new Claim("role", "Filmstudio")

                        };

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

                            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                                _config["Tokens:Audience"],
                                claims,
                                signingCredentials: creds,
                                expires: DateTime.UtcNow.AddMinutes(20));
                            return Created("", new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo
                            });
                        }
                    }
                    else
                    {
                        return BadRequest("You are not a Filmstudio!");

                    }


                }
            }
            return BadRequest();
        }

        // Api/Movie/{id} Put/Patch
        [Authorize]
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

        
        
    }
}
