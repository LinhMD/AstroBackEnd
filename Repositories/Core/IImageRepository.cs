using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Core
{
    public interface IImageRepository : IRepository<ImgLink>
    {
        public ImgLink GetAllImageData(int id);
    }
}
