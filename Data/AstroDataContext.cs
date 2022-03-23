using AstroBackEnd.Models;
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
        private readonly IConfiguration _config;

        public AstroDataContext(IConfiguration configuration)
        {
            this._config = configuration;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Zodiac> Zodiacs { get; set; }

        public DbSet<Planet> Planets { get; set; }

        public DbSet<House> Houses { get; set; }

        public DbSet<PlanetHouse> PlanetHouses { get; set; }

        public DbSet<PlanetZodiac> PlanetZodiacs { get; set; }

        public DbSet<BirthChart> BirthCharts { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Horoscope> Horoscopes { get; set; }

        public DbSet<ZodiacHouse> ZodiacHouses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ImgLink> ImgLinks { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<HoroscopeItem> HoroscopeItems { get; set; }

        public DbSet<Aspect> Aspects { get; set; }

        public DbSet<LifeAttribute> LifeAttributes { get; set; }

        public DbSet<NewsTags> NewsTags { get; set; }

        public DbSet<Topic> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:AstroBackEndContext"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }

        public DbSet<AstroBackEnd.Models.News> News { get; set; }
    }
}
