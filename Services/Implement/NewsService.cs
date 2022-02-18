using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Services.Core;
using AstroBackEnd.RequestModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.NewRequest;

namespace AstroBackEnd.Services.Implement
{
    public class NewsService : INewsService, IDisposable
    {
        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;
        public NewsService(IUnitOfWork work, AstroDataContext astroData)
        {
            this._work = work;
            this._astroData = astroData;
        }
        public News CreateNew(NewsCreateRequest request)
        {
            News news = new News()
            {
                Title = request.Title,
                Description = request.Description, 
                GeneratDate= DateTime.Now,
                Content = request.Content,
                Tag=request.Tag,
                HtmlContent = request.HtmlContent
            };
            return _work.News.Add(news);
        }

        public void DeleteNew(int id)
        {
            _work.News.Remove(GetNews(id));
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<News> FindNews(FindNewsRequest Request)
        {           
            Func<News, bool> filter = news =>
            {
                bool checkTitle = true;
                bool checkDescription = true;
                bool checkTag = true;

                if (!string.IsNullOrWhiteSpace(Request.Title))
                {
                    checkTitle = news.Title.Contains(Request.Title);
                }

                if (!string.IsNullOrWhiteSpace(Request.Description))
                {
                    checkDescription = news.Description.Contains(Request.Description);
                }
                if (!string.IsNullOrWhiteSpace(Request.Tag))
                {
                    checkDescription = news.Description.Contains(Request.Description);
                }

                //checkStatus = user.Status == userRequest.Status;

                return checkTitle && checkDescription && checkTag ;
            };



            IEnumerable<News> neww = _work.News.FindPaging<String>(filter, n => n.Title, Request.Page, Request.PageSize);

            return neww;
        }

        public IEnumerable<News> GetAllNew()
        {
            return _work.News.GetAll<String>(n=>n.Title);
        }

        public News GetNews(int id)
        {
            return _work.News.Get(id);
        }

        public void UpdateNew(int id, NewsUpdateRequest request)
        {
            var updateNew = _work.News.Get(id);
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                updateNew.Title = request.Title;
            }
            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                updateNew.Description = request.Description;
            }
            if (!string.IsNullOrWhiteSpace(request.Content))
            {
                updateNew.Content = request.Content;
            }
            if (!string.IsNullOrWhiteSpace(request.Tag))
            {
                updateNew.Tag = request.Tag;
            }
            if (!string.IsNullOrWhiteSpace(request.HtmlContent))
            {
                updateNew.HtmlContent = request.HtmlContent;
            }

        }
    }
}
