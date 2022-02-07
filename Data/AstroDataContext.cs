﻿using AstroBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Data
{
    public class AstroDataContext : DbContext
    {
        private IConfiguration _config;

        public AstroDataContext(IConfiguration configuration)
        {
            this._config = configuration;
        }

        public DbSet<User> Users { get; set; }


        public DbSet<Profile> Profiles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:AstroBackEndContext"]);
        }

    }
}
