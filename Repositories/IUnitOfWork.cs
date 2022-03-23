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

        IProductRepository Products { get; }

        ICategoryRepository Categories { get; }

        IZodiacRepository Zodiacs { get; }

        IImageRepository Image { get; }
       

        IHouseRepository Houses { get; }

        IZodiacHouseRepository ZodiacHouses { get; }

        IHoroscopeRepository Horoscopes { get; }

        IQuoteRepository Quotes { get; }

        IPlanetRepository Planets { get; }

        IPlanetZodiacRepository PlanetZodiacs { get; }

        IPlanetHouseRepository PlanetHouses { get; }

        IHoroscopeItemRepository HoroscopeItems { get; }

        IAspectRepository Aspects { get; }

        ILifeAttributeRepository LifeAttributes { get; }

        ITopicRepository Topics { get; }
        int Complete();
    }
}
