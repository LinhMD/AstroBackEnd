using AstroBackEnd.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Data
{
    public class AstroDataInit
    {
        public static void Seed(IApplicationBuilder builder)
        {
            using(var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AstroDataContext>();

                var adminRole = new Role()
                {
                    Name = "admin"
                };

                var userRole = new Role(){
                    Name = "user"
                };

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        adminRole, userRole
                    );
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User()
                        {
                            UserName = "LinhMD",
                            Role = adminRole,
                            PhoneNumber = "0359434149",
                            Status = 1
                        },
                        new User()
                        {
                            UserName = "BasicUser",
                            Role = userRole,
                            PhoneNumber = "0359434149",
                            Status = 1
                        }
                    ); 
                }

                var cata1 = new Catagory()
                {
                    Name = "Vòng tay"
                };
                var cata2 = new Catagory()
                {
                    Name = "Áo thun"
                };

                if (!context.Catagories.Any())
                {
                    context.Catagories.AddRange(
                        cata1, cata2
                    );
                }

                if (!context.Products.Any())
                {
                    var master = new Product()
                    {
                        Name = "Áo thun bọ cạp",
                        Catagory = cata2,
                        Detail = "Áo thun dành cho cung bọ cạp",
                        Description = "nah"
                    };
                    context.Products.Add(master);

                    var child1 = new Product()
                    {
                        Name = "Áo thun bọ cạp",
                        Catagory = cata2,
                        Color = "#FF0000",
                        MasterProduct = master
                    };
                    var child2 = new Product()
                    {
                        Name = "Áo thun bọ cạp",
                        Catagory = cata2,
                        Color = "#0000FF",
                        MasterProduct = master
                    };
                    var child3 = new Product()
                    {
                        Name = "Áo thun bọ cạp",
                        Catagory = cata2,
                        Color = "#00FF00",
                        MasterProduct = master
                    };
                    context.Products.AddRange(
                            child1, child2, child3
                        );
                }
                context.SaveChanges();

            }
        }
    }
}
