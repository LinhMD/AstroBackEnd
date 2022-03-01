using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.Utilities;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class Login : ControllerBase
    {
        
        private IConfiguration _config;

        private IUnitOfWork _unitOfWork;

        private FireabaseUtility _fbUtil;
        public Login(IConfiguration config, IUnitOfWork unitOfWork, FireabaseUtility utility)
        {
            this._config = config;
            this._unitOfWork = unitOfWork;
            this._fbUtil = utility;
        }

        [HttpGet]
        [Route("random")]
        [Authorize(Roles = "admin")]
        public int random()
        {
            return new Random().Next();
        }

        [HttpGet("firebase")]
        public async Task<IActionResult> getFireBaseUser(string token)
        {
            try
            {
                UserRecord userRecord = await _fbUtil.getFireBaseUserByToken(token);
                return Ok(userRecord);
            }catch(Exception e)
            {
                return BadRequest(  e.Message);
            }

        }

        // POST api/<Login>
        [HttpPost]
        public IActionResult Post([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);

            }
            return NotFound("Users not found");
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSetting:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Console.WriteLine(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
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
            User user = _unitOfWork.Users.Find(u => u.UserName == userLogin.UserName, u => u.UserName).FirstOrDefault();
            user = _unitOfWork.Users.GetAllUserData(user.Id);
            return user;
        }


    }
}
