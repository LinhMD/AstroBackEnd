using AstroBackEnd.Models;
using System;

namespace AstroBackEnd.RequestModels
{
    public class FindProfileRequest
    {
        public string? Name { get; set; }

        public DateTime? BirthDateStart { get; set; }

        public DateTime? BirthDateEnd { get; set; }
         
        public string? BirthPlace { get; set; }

        public int? ZodiacId { get; set; }
        
        public int? UserId { get; set; }
        public PagingRequest? PagingRequest { get; set; }
    }
}