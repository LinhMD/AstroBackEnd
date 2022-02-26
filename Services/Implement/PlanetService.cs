using AstroBackEnd.Repositories;
using AstroBackEnd.Services.Core;
using System;

namespace AstroBackEnd.Services.Implement
{
    public class PlanetService : IPlanetService, IDisposable
    {
        private readonly IUnitOfWork _work;
        public PlanetService(IUnitOfWork _work)
        {
            this._work = _work;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
