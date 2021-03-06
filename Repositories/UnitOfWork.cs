using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.Repositories.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AstroBackEnd.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AstroDataContext _dataContext;
        public UnitOfWork(AstroDataContext dataContext)
        {
            this._dataContext = dataContext;
            Users = new UserRepository(dataContext);
            Roles = new RoleRepository(dataContext);
            News = new NewsRepository(dataContext);
            Profiles = new ProfileRepository(dataContext);
            Orders = new OrderRepository(dataContext);
            OrderDetails = new OrderDetailRepository(dataContext);
            Products = new ProductRepository(dataContext);
            Categories = new CategoryRepository(dataContext);
            Image = new ImageRepository(dataContext);
            Zodiacs = new ZodiacRepository(dataContext);
            Houses = new HouseRepository(dataContext);
            ZodiacHouses = new ZodiacHouseRepository(dataContext);
            Quotes = new QuoteRepository(dataContext);
            Horoscopes = new HoroscopeRepository(dataContext);
            Planets = new PlanetRepository(dataContext);
            PlanetZodiacs = new PlanetZodiacRepository(dataContext);
            PlanetHouses = new PlanetHouseRepository(dataContext);
            HoroscopeItems = new HoroscopeItemRepository(dataContext);
            Aspects = new AspectRepository(dataContext);
            LifeAttributes = new LifeAttributeRepository(dataContext);
            Topics = new TopicRepository(dataContext);
        }

        public IUserRepository Users { get; }

        public IRoleRepository Roles { get; }

        public INewsRepository News { get; }

        public IProfileRepository Profiles { get; }

        public IProductRepository Products { get; }

        public IOrderRepository Orders { get; }

        public IOrderDetailRepository OrderDetails { get; }

        public ICategoryRepository Categories { get; }


        public IImageRepository Image { get; }

        public IZodiacRepository Zodiacs { get; }

        public IHouseRepository Houses { get; }

        public IZodiacHouseRepository ZodiacHouses { get; }

        public IHoroscopeRepository Horoscopes { get; }

        public IQuoteRepository Quotes { get; }

        public IPlanetRepository Planets { get; }

        public IPlanetZodiacRepository PlanetZodiacs { get; }

        public IPlanetHouseRepository PlanetHouses { get; }

        public IZodiacRepository Zodiac { get; }

        public IHoroscopeItemRepository HoroscopeItems { get; }

        public IAspectRepository Aspects { get; }

        public ILifeAttributeRepository LifeAttributes { get; }

        public ITopicRepository Topics { get; }
        public int Complete()
        {
            return _dataContext.SaveChanges();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
