﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AstroBackEnd.Data;
using AstroBackEnd.Models;

namespace AstroBackEnd.Pages.planet
{
    public class CreateModel : PageModel
    {
        private readonly AstroBackEnd.Data.AstroDataContext _context;

        public CreateModel(AstroBackEnd.Data.AstroDataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Planet Planet { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Planets.Add(Planet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}