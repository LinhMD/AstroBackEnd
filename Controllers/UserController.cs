
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
using System.IO;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;
        private IProfileService _profileService;
        private IUnitOfWork _work;
        private IAspectService _aspectService;
        private IFirebaseService _firebase;

        public UserController(IUserService userService, IUnitOfWork work, IProfileService profileService, IAspectService aspect, IFirebaseService firebase)
        {
            this._userService = userService;
            this._work = work;
            this._profileService = profileService;
            this._aspectService = aspect;
            this._firebase = firebase;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUser(int id)
        {
            try
            {

                return Ok(_userService.GetUser(id));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateUser([FromBody] UserCreateRequest request)
        {
            try
            {
                
                return Ok(_userService.CreateUser(request));
                
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateUser(int id, [FromBody] UserUpdateRequest request)
        {
            try
            {
                _userService.UpdateUser(id, request);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// there is no summary
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "admin")]
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
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
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
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
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
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("current/profiles/{profileId}/horoscope")]
        [Authorize]
        public IActionResult GetHoroscopeDaily(int profileId, DateTime date)
        {

            try
            {
                int id = GetCurrentUserId();
                User user = _userService.GetUser(id);
                Profile profile = user.Profiles.FirstOrDefault(p => p.Id == profileId);
                if (profile == null) throw new ArgumentException("Profile Id not found");
                return Ok(_aspectService.CalculateAspect(profile.BirthDate, date));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
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
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
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
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
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
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
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
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
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
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
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
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("image")]
        public IActionResult UploadImage(IFormFile file)
        {
            using Stream stream = file.OpenReadStream();
            Console.WriteLine(file.Name);
            Console.WriteLine(file.ContentType);
            Console.WriteLine(file.FileName);
            string fileFormat = file.FileName.Split('.').Last();
            Console.WriteLine(fileFormat);
            
            if (fileFormat == "png" || fileFormat == "jpg" || fileFormat == "svg")
            {
                string fileName = Guid.NewGuid().ToString() + "." + fileFormat;

                Task<string> task = _firebase.UploadImage(stream, fileName);
                return Ok(task.Result);
            }

            return BadRequest("File must be a image!!");
        }
    }
}
