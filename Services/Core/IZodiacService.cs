﻿using AstroBackEnd.Models;
using AstroBackEnd.RequestModels;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IZodiacService
    {

        public Zodiac CreateZodiac(CreateZodiacRequest request);

        public string RemoveZodiac(int id);

        public Zodiac UpdateZodiac(int id, UpdateZodiacRequest updateZodiac);

        public IEnumerable<Zodiac> FindZodiac(FindZodiacRequest request);
    }
}
