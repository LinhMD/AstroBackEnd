using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class User
    {
        public User()
        {

        }


        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { set; get; }


        [Required] 
        public Role Role { get; set; }

        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Must be phone number")]
        public string? PhoneNumber { get; set; }

        public int Status { get; set; }

        public IList<Profile> Profiles { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
