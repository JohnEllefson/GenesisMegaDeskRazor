using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GenesisMegaDeskRazor.Data;
using GenesisMegaDeskRazor.Models;

namespace GenesisMegaDeskRazor.Pages.DeskQuotes
{
    public class DeleteModel : PageModel
    {
        private readonly GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext _context;

        public DeleteModel(GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DeskQuote DeskQuote { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            var deskquote = await _context.DeskQuote.FirstOrDefaultAsync(m => m.Id == id);

            if (deskquote == null)
            {
                return NotFound();
            }
            else 
            {
                DeskQuote = deskquote;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }
            var deskquote = await _context.DeskQuote.FindAsync(id);

            if (deskquote != null)
            {
                DeskQuote = deskquote;
                _context.DeskQuote.Remove(DeskQuote);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
