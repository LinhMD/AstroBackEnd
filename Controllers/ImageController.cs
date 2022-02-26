using AstroBackEnd.Services.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        [HttpDelete]
        public IActionResult DeleteLink(string link)
        {
            _imageService.DeleteImage(link);
            return Ok();
        }
    }
}
