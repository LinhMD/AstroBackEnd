using AstroBackEnd.Models;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.ProfileRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/profiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public IActionResult GetProfile(int id)
        {
            
            try
            {

                var profile = _profileService.GetProfile(id);
                return Ok(profile);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpGet("{id}/chart")]
        public IActionResult GetBirthChart(int id)
        {
            try
            {
                return Ok(_profileService.GetBirthChart(id)); 
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CreateProfile(CreateProfileRequest request)
        {
            try
            {
                return Ok(_profileService.CreateProfile(request));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult FindProfile(string? name, int? userId,DateTime? birthDateStart, DateTime? birthDateEnd, string? birthPlace, int? zodiacId, string? sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                IEnumerable<Profile> profiles = _profileService.FindProfile(new FindProfileRequest()
                {
                    Name = name,
                    BirthDateStart = birthDateStart,
                    BirthDateEnd = birthDateEnd,
                    BirthPlace = birthPlace,
                    ZodiacId = zodiacId,
                    UserId = userId,

                    PagingRequest = new PagingRequest()
                    {
                        Page = page,
                        PageSize = pageSize,
                        SortBy = sortBy
                    }
                }, out int total);


                return Ok(new PagingView() { Payload = profiles, Total = total } );
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult UpdateProfile(int id, UpdateProfileRequest request)
        {
            try
            {
                Profile updateProfile = _profileService.UpdateProfile(id, request);

                return Ok(updateProfile);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProfile(int id)
        {
            try
            {
                _profileService.DeleteProfile(id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            } 
        }
    }
}
