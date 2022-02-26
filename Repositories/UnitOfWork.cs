using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.Repositories.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Catagory = new CatagoryRepository(dataContext);
            Zodiacs = new ZodiacRepository(dataContext);
            Houses = new HouseRepository(dataContext);
            ZodiacHouses = new ZodiacHouseRepository(dataContext);
            Quotes = new QuoteRepository(dataContext);
            Horoscopes = new HoroscopeRepository(dataContext);
            Planets = new PlanetRepository(dataContext);

        }
        public IUserRepository Users { get; }

        public IRoleRepository Roles { get; }

        public INewsRepository News { get; }

        public IProfileRepository Profiles { get; }

        public IOrderRepository Orders { get; }

        public IOrderDetailRepository OrderDetails { get; }

        public IProductRepository Products { get; }

        public ICatagoryRepository Catagory { get; }

        public IZodiacRepository Zodiacs { get; }

        public IHouseRepository Houses { get; }

        public IZodiacHouseRepository ZodiacHouses { get; }

        public IHoroscopeRepository Horoscopes { get; }

        public IQuoteRepository Quotes { get; }

        public IPlanetRepository Planets { get; }

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
