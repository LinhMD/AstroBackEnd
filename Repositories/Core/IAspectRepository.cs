using AstroBackEnd.Models;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Repositories.Core
{
    public interface IAspectRepository : IRepository<Aspect>
    {
        public Aspect GetAspectWithAllData(int id);

        public IEnumerable<Aspect> FindAspectWithAllData<TSortBy>(Func<Aspect, bool> filter, Func<Aspect, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20);

        public IEnumerable<Aspect> FindAspectWithAllData<TSortBy>(Func<Aspect, bool> filter, Func<Aspect, TSortBy> sortBy, out int total);
    }
}
