using AstroBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstroBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {

        private IConfiguration _config;

        public Login(IConfiguration config)
        {
            this._config = config;
        }

        [HttpGet]
        [Route("random")]
        [Authorize(Roles = "admin")]
        public int random()
        {
            return new Random().Next();
        }
        // POST api/<Login>
        [HttpPost]
        public IActionResult Post([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                user.Token = token;
                JsonResult result = new JsonResult(user);
                return result;
            }
            return NotFound("Users not found");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSetting:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
            };
            var token = new JwtSecurityToken(_config["JwtSetting:Issuer"],
                                            _config["JwtSetting:Audience"],
                                            claims,
                                            expires: DateTime.Now.AddSeconds(Double.Parse(_config["JwtSetting:Expirseconds"])),
                                            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //TODO: implement login
        private User Authenticate(UserLogin userLogin)
        {
            User user = Models.User.Users.FirstOrDefault(
                            u => u.UserName.ToLower() == userLogin.UserName.ToLower()
                        );

            if (user != null)
                return user;

            return null;

        }
    }
}
