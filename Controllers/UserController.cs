
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
using AstroBackEnd.Utilities;
using AstroBackEnd.RequestModels.UserRequest;
using AstroBackEnd.ViewsModel;
using AstroBackEnd.RequestModels.ProfileRequest;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;
        private IProfileService _profileService;
        private IUnitOfWork _work;

        public UserController(IUserService userService, IUnitOfWork work, IProfileService profileService)
        {
            this._userService = userService;
            this._work = work;
            this._profileService = profileService;
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

        [HttpPut]

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

        /// <summary>
        /// there is no summary
        /// </summary>
        [HttpGet]
        public IActionResult FindUsers(string? name, string? phone, string? sortBy, int? status,  int page = 1, int pageSize = 20)
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
                int total = 0;

                var users = _userService.FindUsers(request, out total);

                PagingView view = new PagingView()
                {
                    Payload = users,
                    Total = total
                };

                return Ok(view);
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
                int id = GetCurrentUserId();

                User user = _work.Users.GetAllUserData(id);
                return Ok(user);
            }
            catch (Exception e) 
            { 
                return BadRequest(e.Message); 
            }
        }

        private int GetCurrentUserId()
        {
            ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int id = int.Parse(userId);
            return id;
        }

        [HttpGet("current/profiles")]
        [Authorize]
        public IActionResult GetUserProfiles()
        {
            
            try
            {
                int id = GetCurrentUserId();
                User user = _userService.GetUser(id);
                return Ok(user.Profiles);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("current/profiles/{profileId}")]
        [Authorize]
        public IActionResult GetUserProfile(int profileId)
        {

            try
            {
                int id = GetCurrentUserId();
                User user = _userService.GetUser(id);
                Profile profile = user.Profiles.FirstOrDefault(p => p.Id == profileId);
                if (profile == null) throw new ArgumentException("Profile Id not found");

                profile = _profileService.GetProfile(profileId);
                return Ok(profile);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("current/profiles/{profileId}/chart")]
        [Authorize]
        public IActionResult GetUserBirthChart(int profileId)
        {

            try
            {
                int id = GetCurrentUserId();
                User user = _userService.GetUser(id);
                Profile profile = user.Profiles.FirstOrDefault(p => p.Id == profileId);

                if (profile == null) 
                    throw new ArgumentException("Profile Id not found");

                BirthChartView birthChartView = _profileService.GetBirthChart(profileId);

                return Ok(birthChartView);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("current/profiles")]
        [Authorize]
        public IActionResult AddProfile(CreateProfileRequest request)
        {
            try
            {
                int id = GetCurrentUserId();
                if (request.UserId != id) throw new ArgumentException("User id diffirent with current user id");
                Profile profile = _profileService.CreateProfile(request);
                return Ok(profile);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("current/profiles/{profileId}")]
        [Authorize]
        public IActionResult DeleteProfile(int profileId)
        {
            try
            {
                int id = GetCurrentUserId();
                User user = _userService.GetUser(id);
                Profile profile = user.Profiles.FirstOrDefault(p => p.Id == profileId);

                if (profile == null)
                    throw new ArgumentException("Profile Id not found");
                _profileService.DeleteProfile(profile.Id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("current/profiles/{profileId}")]
        [Authorize]
        public IActionResult UpdateProfile(UpdateProfileRequest request, int profileId)
        {
            try
            {
                int id = GetCurrentUserId();
                User user = _userService.GetUser(id);
                Profile profile = user.Profiles.FirstOrDefault(p => p.Id == profileId);

                if (profile == null)
                    throw new ArgumentException("Profile Id not found");

                profile = _profileService.UpdateProfile(profileId, request);
                return Ok(profile);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get Cart of the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("cart")]
        [Authorize]
        public IActionResult GetUserCart()
        {

            try
            {
                ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
                string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int id = int.Parse(userId);

                Order cart = _userService.getCart(id);

                return Ok(cart);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("cart")]
        [Authorize]
        public IActionResult AddItemToCart([FromBody]AddToCartRequest request)
        {
            try
            {
                ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
                string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int id = int.Parse(userId);

                Order cart = _userService.AddToCart(id, request);

                return Ok(cart);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
