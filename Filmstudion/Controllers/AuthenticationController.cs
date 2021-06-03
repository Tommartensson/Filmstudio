using AutoMapper;
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
    public class AuthenticationController : ControllerBase
    {
 
        private readonly SignInManager<MovieCoModel> _sign;
        private readonly UserManager<MovieCoModel> _userCo;
        private readonly IConfiguration _config;

        public AuthenticationController(
            SignInManager<MovieCoModel> sign,
            UserManager<MovieCoModel> userCo,
            IConfiguration config)
        {
            _sign = sign;
            _userCo = userCo;
            _config = config;
        }
        public async Task<IActionResult> CreateToken([FromBody] MovieCoModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userCo.FindByNameAsync(model.name);
                if (user != null)
                {
                    var result = await _sign.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.name),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.name)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));

                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_config["Token:Issuer"],
                            _config["Token:Audience"],
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

            }
            return BadRequest();
        }
    }
}
