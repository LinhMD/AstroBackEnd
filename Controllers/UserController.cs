
using AstroBackEnd.Services.Core;
using AstroBackEnd.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
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


    }
}
