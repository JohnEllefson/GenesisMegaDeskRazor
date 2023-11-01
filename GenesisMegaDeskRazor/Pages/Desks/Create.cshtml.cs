using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GenesisMegaDeskRazor.Data;
using GenesisMegaDeskRazor.Models;
using Newtonsoft.Json;

namespace GenesisMegaDeskRazor.Pages.Desks
{
    public class CreateModel : PageModel
    {
        private readonly GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext _context;

        public CreateModel(GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Desk Desk { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

                   
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid || _context.Desk == null || Desk == null)
            {
                return Page();
            }

            _context.Desk.Add(Desk);
            await _context.SaveChangesAsync();
            int deskId = Desk.Id;

            string url = Url.Page("../DeskQuotes/Create", new { deskId = deskId });

            return Redirect(url);
        }
    }
}
