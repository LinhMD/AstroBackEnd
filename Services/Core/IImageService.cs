using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.ImageRequest;

namespace AstroBackEnd.Services.Core
{
    public interface IImageService
    {
        public ImgLink GetImage(int id);

        public IEnumerable<ImgLink> GetAllImage();

        public IEnumerable<ImgLink> FindImage(FindImageRequest Request);

        public ImgLink DeleteImage(int id);

        public ImgLink UpdateImage(int id, ImageUpdateRequest request);

        public ImgLink CreateImage(ImageCreateRequest request);

    }
}
