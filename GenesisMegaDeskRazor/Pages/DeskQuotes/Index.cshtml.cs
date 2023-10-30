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
    public class IndexModel : PageModel
    {
        private readonly GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext _context;

        public IndexModel(GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DeskQuote != null)
            {
                DeskQuote = await _context.DeskQuote.ToListAsync();
            }
        }
    }
}
