﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.ImageRequest
{
    public class ImageUpdateRequest
    {
        public string Link { get; set; }

        public int ProductId { get; set; }
    }
}
