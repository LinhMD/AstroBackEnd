using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.NewRequest;

namespace AstroBackEnd.Services.Core
{
    public interface INewsService
    {
        public News GetNews(int id);

        public IEnumerable<News> GetAllNew();

        public IEnumerable<News> FindNews(FindNewsRequest Request);

        public IEnumerable<News> FindNews(FindNewsRequest Request, out int total);

        public News DeleteNew(int id);

        public News UpdateNew(int id, NewsUpdateRequest request);

        public News CreateNew(NewsCreateRequest news);

    }
}
