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
        public string UserName { set; get; }

        public string? Token { get; set; }

        [Required]
        public string Role { get; set; }

        public ICollection<Profile> Profiles { get; set; }

        public static List<User> Users = new()
        {
            new User()
            {
                Id = 1, UserName="admin",  Role="admin"
            },
            new User()
            {
                Id = 2, UserName="normie", Role="normie"
            }
        };
    }
}
