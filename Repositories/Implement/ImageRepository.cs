using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Implement
{
    public class ImageRepository : Repository<ImgLink>, IImageRepository
    {
        public ImageRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }
        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public Product GetAllImageData(int id)
        {
            return AstroData.Products.Include("Link").Include("ProductId")
                .First(p => p.Id == id);
        }
    }
}
