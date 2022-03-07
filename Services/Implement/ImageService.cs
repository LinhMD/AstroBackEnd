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
using AstroBackEnd.Utilities;

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
            try
            {
                ImgLink image = new ImgLink()
                {
                    Link = request.Link,
                    ProductId = request.ProductId
                };
                return _work.Image.Add(image);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("ImageService : " + ex.Message);
            }
        }

        public ImgLink DeleteImage(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            ImgLink imgLink = _work.Image.Get(id);
            if (imgLink != null)
            {
                _work.Image.Remove(GetImage(id));
                return imgLink;
            }
            else
            {
                throw new ArgumentException("This ZodiacHouse not found");
            }
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<ImgLink> FindImage(FindImageRequest Request)
        {
            if (Request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
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
            catch (Exception ex) { throw new ArgumentException("ImageService : " + ex.Message); }
        }

        public IEnumerable<ImgLink> GetAllImage()
        {
            return _work.Image.GetAll<String>(p => p.Link);
        }

        public ImgLink GetImage(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            ImgLink imgLink = _work.Image.Get(id);
            if (imgLink != null)
            {
                return imgLink;
            }
            else { throw new ArgumentException("This imgLink not found"); }
        }

        public ImgLink UpdateImage(int id, ImageUpdateRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            ImgLink imgLink = _work.Image.Get(id);
            if (imgLink != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Link))
                {
                    imgLink.Link = request.Link;
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(request.ProductId)))
                {
                    imgLink.ProductId = request.ProductId;
                }
                return imgLink;
            }
            else
            {
                throw new ArgumentException("This imgLink not found");
            }
        }
    }
}
