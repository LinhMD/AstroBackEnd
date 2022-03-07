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
using AstroBackEnd.Utilities;

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
            try
            {
                News news = new News()
                {
                    Title = request.Title,
                    Description = request.Description,
                    GeneratDate = DateTime.Now,
                    Content = request.Content,
                    Tag = request.Tag,
                    HtmlContent = request.HtmlContent
                };
                return _work.News.Add(news);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("NewsService : " + ex.Message);
            }
        }

        public News DeleteNew(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            News news = _work.News.Get(id);
            if (news != null)
            {
                _work.News.Remove(GetNews(id));
                return news;
            }
            else
            {
                throw new ArgumentException("This news not found");
            }
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<News> FindNews(FindNewsRequest Request)
        {
            if (Request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<News, bool> filter = news =>
                {
                    bool checkId = Request.Id == null ? true : news.Id == Request.Id;
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

                    return checkTitle && checkDescription && checkTag;
                };
                IEnumerable<News> result = null;
                if (Request.PagingRequest != null)
                {
                    switch (Request.PagingRequest.SortBy)
                    {
                        case "Id":
                            result = _work.News.FindPaging(filter, p => p.Id, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;
                        case "Title":
                            result = _work.News.FindPaging(filter, p => p.Title, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;
                        case "ZodiacId":
                            result = _work.News.FindPaging(filter, p => p.Description, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;

                        case "Tag":
                            result = _work.News.FindPaging(filter, p => p.Tag, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;
                        default:
                            result = _work.News.FindPaging(filter, p => p.Id, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;
                    }
                }
                else
                {
                    result = _work.News.Find(filter, p => p.Title);
                }



                return result;
            }
            catch (Exception ex) { throw new ArgumentException("NewsService : " + ex.Message); }
        }

        public IEnumerable<News> FindNews(FindNewsRequest Request, out int total)
        {
            if (Request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<News, bool> filter = news =>
                {
                    bool checkId = Request.Id == null ? true : news.Id == Request.Id;
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

                    return checkTitle && checkDescription && checkTag;
                };
                IEnumerable<News> result = null;
                if (Request.PagingRequest != null)
                {
                    switch (Request.PagingRequest.SortBy)
                    {
                        case "Id":
                            result = _work.News.FindPaging(filter, p => p.Id, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;
                        case "Title":
                            result = _work.News.FindPaging(filter, p => p.Title, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;
                        case "ZodiacId":
                            result = _work.News.FindPaging(filter, p => p.Description, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;

                        case "Tag":
                            result = _work.News.FindPaging(filter, p => p.Tag, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;
                        default:
                            result = _work.News.FindPaging(filter, p => p.Id, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                            break;
                    }
                }
                else
                {
                    result = _work.News.Find(filter, p => p.Title);
                    total = result.Count();
                }
                total = result.Count();


                return result;
            }
            catch (Exception ex) { throw new ArgumentException("NewsService : " + ex.Message); }
        }

        public IEnumerable<News> GetAllNew()
        {
            return _work.News.GetAll<String>(n => n.Title);
        }

        public News GetNews(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            News news = _work.News.Get(id);
            if (news != null)
            {
                return news;
            }
            else { throw new ArgumentException("This news not found"); }
        }

        public News UpdateNew(int id, NewsUpdateRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            News news = _work.News.Get(id);
            if (news != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    news.Title = request.Title;
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    news.Description = request.Description;
                }
                if (!string.IsNullOrWhiteSpace(request.Content))
                {
                    news.Content = request.Content;
                }
                if (!string.IsNullOrWhiteSpace(request.Tag))
                {
                    news.Tag = request.Tag;
                }
                if (!string.IsNullOrWhiteSpace(request.HtmlContent))
                {
                    news.HtmlContent = request.HtmlContent;
                }
                return news;
            }
            else
            {
                throw new ArgumentException("This news not found");
            }
        }
    }
}
