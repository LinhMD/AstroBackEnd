using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AstroBackEnd.Data;
using AstroBackEnd.Models;

namespace AstroBackEnd.Pages.planet
{
    public class IndexModel : PageModel
    {
        private readonly AstroBackEnd.Data.AstroDataContext _context;

        public IndexModel(AstroBackEnd.Data.AstroDataContext context)
        {
            _context = context;
        }

        public IList<Planet> Planet { get;set; }

        public async Task OnGetAsync()
        {
            Planet = await _context.Planets.ToListAsync();
        }
    }
}
