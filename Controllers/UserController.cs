﻿
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

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                return Ok(_userService.GetUser(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequest request)
        {
            try
            {
                return Ok(_userService.CreateUser(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut("{id}")]

        public IActionResult UpdateUser(int id, [FromBody] UserUpdateRequest request)
        {
            try
            {
                _userService.UpdateUser(id, request);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult FindUsers(string? name, string? phone, string? sortBy,int status = 1,  int page = 1, int pageSize = 20)
        {
            try
            {
                FindUserRequest request = new FindUserRequest()
                {
                    Name = name,
                    Phone = phone,
                    Status = status,
                    PagingRequest = new PagingRequest()
                    {
                        SortBy = sortBy,
                        Page = page,
                        PageSize = pageSize
                    }

                };

                var users = _userService.FindUsers(request);
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("current")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            try
            {
                ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
                string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int id = int.Parse(userId);
                User user = _work.Users.GetAllUserData(id);
                return Ok(user);
            }
            catch (Exception e) 
            { 
                return BadRequest(e.Message); 
            }
        }

        [HttpGet("cart")]
        public IActionResult GetUserCart()
        {


            return null;
        }
    }
}
