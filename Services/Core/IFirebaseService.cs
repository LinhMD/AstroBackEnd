using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Core
{
    public interface IFirebaseService
    {
        public Task<string> UploadImage(Stream image, string name);
    }
}
