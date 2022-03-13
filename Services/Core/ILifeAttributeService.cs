using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.LifeAttributeRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface ILifeAttributeService
    {
        public LifeAttribute CreateLifeAttribute(CreateLifeAttributeRequest createLifeAttribute);
        public LifeAttribute GetLifeAttribute(int id);
        public IEnumerable<LifeAttribute> FindLifeAttribute(FindLifeAttributeRequest findLifeAttribute, out int total);
        public LifeAttribute UpdateLifeAttribute(int id, UpdateLifeAttributeRequest updateLifeAttribute);
        public LifeAttribute DeleteLifeAttribute(int id);
    }
}
