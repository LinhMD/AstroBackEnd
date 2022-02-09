using System;
using System.ComponentModel.DataAnnotations;

namespace AstroBackEnd.Models
{
    public class Profile
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string ProfilePhoto { get; set; }

        public Zodiac Zodiac { get; set; }

        public BirthChart BirthChart { get; set; }

    }
}