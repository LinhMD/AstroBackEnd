using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.ProfileRequest
{
    public class UpdateProfileRequest
    {
        public string? Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? BirthPlace { get; set; }

        public double? Latitude { get; set; }

        public double? Longtitude { get; set; }

        public string? ProfilePhoto { get; set; }

    }
}
