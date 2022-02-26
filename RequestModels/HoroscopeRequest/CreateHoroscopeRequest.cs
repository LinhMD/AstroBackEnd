using AstroBackEnd.Models;
using System.Collections.Generic;

namespace AstroBackEnd.RequestModels.HoroscopeRequest
{
    public class CreateHoroscopeRequest
    {
        public string ColorLuck { get; set; }

        public float NumberLuck { get; set; }

        public string Work { get; set; }

        public string Love { get; set; }

        public string Money { get; set; }
    }
}
