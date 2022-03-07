using AstroBackEnd.Models;
using AstroBackEnd.RequestModels;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{id}")]
        public IActionResult GetProfile(int id)
        {
            var profile = _profileService.GetProfile(id);
            if(profile != null)
            {
                return Ok(profile);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateProfile(CreateProfileRequest request)
        {
            try
            {
                return Ok(_profileService.CreateProfile(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet]
        public IActionResult FindProfile(string? name, DateTime? birthDateStart, DateTime? birthDateEnd, string? birthPlace, int? zodiacId, string? sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                int total = 0;
                IEnumerable<Profile> profiles = _profileService.FindProfile(new FindProfileRequest()
                {
                    Name = name,
                    BirthDateStart = birthDateStart,
                    BirthDateEnd = birthDateEnd,
                    BirthPlace = birthPlace,
                    ZodiacId = zodiacId,
                    PagingRequest = new PagingRequest()
                    {
                        Page = page,
                        PageSize = pageSize,
                        SortBy = sortBy
                    }
                }, out total);


                return Ok(new PagingView() { Payload = profiles, Total = total } );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateProfile(int id, CreateProfileRequest request)
        {
            try
            {
                Profile updateProfile = _profileService.UpdateProfile(id, request);

                return Ok(updateProfile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfile(int id)
        {
            try
            {
                _profileService.DeleteProfile(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            } 
        }
    }
}
