
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

            
        [HttpPost]
        [Route("/")]
        public IActionResult GetUser([FromBody] UserRequest request)
        {
            

            return Ok("hello there");
        }

    }
}
