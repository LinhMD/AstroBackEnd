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
using AstroBackEnd.RequestModels.ImageRequest;

namespace AstroBackEnd.Services.Implement
{
    public class ImageService : IImageService, IDisposable
    {
        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;
        public ImageService(IUnitOfWork work, AstroDataContext astroData)
        {
            this._work = work;
            this._astroData = astroData;
        }

        public void DeleteImage(string link)
        {
            IEnumerable<ImgLink> removeList = this._work.Image.Find(i => i.Link.Equals(link), i => i.Id);
            this._work.Image.RemoveAll(removeList);
        }


        public ImgLink CreateImage(ImageCreateRequest request)
        {
            var pro = _work.Products.Get(request.ProductId);
            ImgLink image = new ImgLink()
            {
                Link = request.Link,
                ProductId = request.ProductId
            };
            return _work.Image.Add(image);
        }

        public void DeleteImage(int id)
        {
            _work.Image.Remove(_work.Image.Get(id));
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<ImgLink> FindImage(FindImageRequest Request)
        {
            Func<ImgLink, bool> filter = p =>
            {
                bool checkLink = true;
                if (!string.IsNullOrWhiteSpace(Request.Link))
                {
                    checkLink = p.Link.Contains(Request.Link);
                }
                bool checkProId = true;
                checkProId = Request.ProductId == null ? true : p.ProductId == Request.ProductId;

                return checkLink && checkProId;
            };
            IEnumerable<ImgLink> result = null;
            if (Request.PagingRequest != null)
            {
                switch (Request.PagingRequest.SortBy)
                {
                    case "Link":
                        result = _work.Image.FindPaging(filter, p => p.Link, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                        break;
                    case "ProductId":
                        result = _work.Image.FindPaging(filter, p => p.ProductId, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                        break;
                    default:
                        result = _work.Image.FindPaging(filter, p => p.Link, Request.PagingRequest.Page, Request.PagingRequest.PageSize);
                        break;

                }
            }
            else
            {
                result = _work.Image.Find(filter, p => p.Link);
            }

            return result;
        }

        public IEnumerable<ImgLink> GetAllImage()
        {
            return _work.Image.GetAll<String>(p => p.Link);
        }

        public ImgLink GetImage(int id)
        {
            return _work.Image.Get(id);
        }

        public ImgLink UpdateImage(int id, ImageUpdateRequest request)
        {
            var image = _work.Image.Get(id);
            if (!string.IsNullOrWhiteSpace(request.Link))
            {
                image.Link = request.Link;
            }
            if (!string.IsNullOrWhiteSpace(Convert.ToString(request.ProductId)))
            {
                image.ProductId = request.ProductId;
            }
            return image;
        }
    }
}
