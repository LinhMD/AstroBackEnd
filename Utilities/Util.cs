using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AstroBackEnd.Utilities
{
    public class Util
    {
        private IUnitOfWork _work;


        public User GetCurrentUser(ControllerBase controller)
        {
            ClaimsIdentity claimsIdentity = controller.User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int id = int.Parse(userId);
            User user = _work.Users.Get(id);
            return user;
        }
    }
}
