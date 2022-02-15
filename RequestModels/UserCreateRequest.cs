using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels
{
    public class UserCreateRequest
    {
        [MaxLength(255)]
        public string UserName { get; set; }

        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be phone number")]
        public string PhoneNumber { get; set; }

    }
}
