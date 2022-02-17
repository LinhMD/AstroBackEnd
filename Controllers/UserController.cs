
using AstroBackEnd.Services.Core;
using AstroBackEnd.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using AstroBackEnd.Repositories;
using AstroBackEnd.Models;
using Microsoft.AspNetCore.Authorization;

namespace AstroBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;
        private IUnitOfWork _work;

        public UserController(IUserService userService, IUnitOfWork work)
        {
            this._userService = userService;
            this._work = work;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            return Ok(_userService.GetAllUser());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(_userService.GetUser(id));
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequest request)
        {
            return Ok(_userService.CreateUser(request));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id,[FromBody] UserCreateRequest request)
        {
            _userService.UpdateUser(id, request);
            return Ok();
        }

        [HttpPost]
        [Route("f")]
        public IActionResult FindUsers([FromBody]FindUserRequest request)
        {
            var users = _userService.FindUsers(request);
            return Ok(users);
        }

        [HttpGet("current")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int id = int.Parse(userId);
            User user = _work.Users.GetAllUserData(id);
            return Ok(user);
        }
    }
}
