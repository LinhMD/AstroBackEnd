using AstroBackEnd.Models;
using AstroBackEnd.RequestModels;
using AstroBackEnd.Services.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AstroBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public IActionResult GetAllProfiles()
        {
            try
            {
                return Ok(_profileService.GetAllProfile());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
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

        [HttpPost]
        [Route("f")]
        public IActionResult FindProfile(FindProfileRequest request)
        {
            try
            {
                return Ok(_profileService.FindProfile(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut("{id}")]
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
