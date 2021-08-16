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
 
        private readonly SignInManager<AdminModel> _sign;
        private readonly UserManager<AdminModel> _userCo;
        private readonly IConfiguration _config;

        public AuthenticationController(
            SignInManager<AdminModel> sign,
            UserManager<AdminModel> userCo,
            IConfiguration config)
        {
            _sign = sign;
            _userCo = userCo;
            _config = config;
        }

        
        [Route("CreateAdmin")]
        [HttpPost]
        public async Task<ActionResult> CreateAdmin([FromBody] AdminModel Adminmodel)
        {

            AdminModel admin = new AdminModel()
            {
                UserName = Adminmodel.UserName,
                password = Adminmodel.password,
                isAdmin = true,
            };
            var create = await _userCo.CreateAsync(admin, admin.password);
           
           
  
            if (!create.Succeeded)
            {
                return BadRequest(create);
            }
            
            return Created("", "Admin skapad");
            
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] AdminModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userCo.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _sign.CheckPasswordSignInAsync(user, model.password, true);

                    if (result.Succeeded)
                    {
                        if (user.isAdmin == true)
                        {
                            var claims = new[]
                            {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                            new Claim("role", "Admin")

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
                        return BadRequest("You are not an Admin!");
                          
                    }
                    
                
                }
            }
            return BadRequest();
        }
    }
}
