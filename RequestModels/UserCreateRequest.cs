using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels
{
    public class UserCreateRequest
    {
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

    }
}
