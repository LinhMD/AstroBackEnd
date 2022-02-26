using AstroBackEnd.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IRoleRepository Roles { get; }

        INewsRepository News { get; }

        IProfileRepository Profiles { get; }

        IOrderRepository Orders { get; }

        IOrderDetailRepository OrderDetails { get; }

        IProductRepository Product { get; }

        ICatagoryRepository Catagory { get; }

        IZodiacRepository Zodiacs { get; }


        IImgLinksRepository ImgLinks { get; }

        IImageRepository Image { get; }
        
        IZodiacProductRepositorye ZodiacProduct { get; }

        IHouseRepository Houses { get; }

        IZodiacHouseRepository ZodiacHouses { get; }


        int Complete();

        
    }
}
