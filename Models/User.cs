using System;
using System.Collections.Generic;
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

        public string UserName { set; get; }

        public string Token { get; set; }
        public string Role { get; set; }

        public string Password { get; set; }

        public static List<User> Users = new()
        {
            new User()
            {
                Id = 1, UserName="admin", Password="addminPassword", Role="admin"
            },
            new User()
            {
                Id = 2, UserName="normie", Password="normie", Role="normie"
            }
        };
    }
}
