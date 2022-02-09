using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AstroBackEnd.Data;
using AstroBackEnd.Models;

namespace AstroBackEnd.Pages.zodiac
{
    public class DeleteModel : PageModel
    {
        private readonly AstroBackEnd.Data.AstroDataContext _context;

        public DeleteModel(AstroBackEnd.Data.AstroDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Zodiac Zodiac { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Zodiac = await _context.Zodiacs.FirstOrDefaultAsync(m => m.Id == id);

            if (Zodiac == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Zodiac = await _context.Zodiacs.FindAsync(id);

            if (Zodiac != null)
            {
                _context.Zodiacs.Remove(Zodiac);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
