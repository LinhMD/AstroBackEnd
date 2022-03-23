using AstroBackEnd.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace AstroBackEnd.RequestModels
{
    public class CreateProfileRequest
    {
        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public double Latitude { get; set; }

        public bool Gender { get; set; }

        public double Longtitude { get; set; }

        public string ProfilePhoto { get; set; }

        public int UserId { get; set; }

    }
}