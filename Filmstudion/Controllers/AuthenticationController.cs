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
    public class AuthenticationController : ControllerBase
    {
 
        private readonly SignInManager<Admin> _sign;
        private readonly UserManager<Admin> _userCo;
        private readonly IConfiguration _config;

        public AuthenticationController(
            SignInManager<Admin> sign,
            UserManager<Admin> userCo,
            IConfiguration config)
        {
            _sign = sign;
            _userCo = userCo;
            _config = config;
        }

        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<ActionResult<AdminModel>> CreateAdmin([FromBody] AdminModel model)
        {

            Admin admin = new Admin()
            {
                Username = model.Username,
                password = model.password
            };
            var snabel = await _userCo.CreateAsync(admin, admin.password);
            if (snabel != IdentityResult.Success)
            {
                throw new InvalidOperationException("Kan inte lägga till admin");
            }
            return Ok();
            
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] AdminModel model)
        {
            model.Username = "Snabel";
            model.password = "P@ssW0rd!";

            Admin admin = new Admin()
            {
                Username = model.Username,
                password = model.password
            };
            var snabel = await _userCo.CreateAsync(admin, model.password);
            if (snabel != IdentityResult.Success)
            {
                throw new InvalidOperationException("Kan inte lägga till admin");
            }
            if (ModelState.IsValid)
            {
                var user = await _userCo.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var result = await _sign.CheckPasswordSignInAsync(user, model.password, true);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
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
