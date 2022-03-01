using AstroBackEnd.Data;
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
            Product = new ProductRepository(dataContext);
            Catagory = new CatagoryRepository(dataContext);
            Image = new ImageRepository(dataContext);
            Zodiac = new ZodiacRepository(dataContext);
        }
        public IUserRepository Users { get; }

        public IRoleRepository Roles { get; }

        public INewsRepository News { get; }

        public IProfileRepository Profiles { get; }

        public IProductRepository Product { get; }

        public IOrderRepository Orders { get; }

        public IOrderDetailRepository OrderDetails { get; }

        public ICatagoryRepository Catagory { get; }

        public IImageRepository Image { get; }

        public IZodiacProductRepositorye ZodiacProduct { get; }

        public IZodiacRepository Zodiac { get; }


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
