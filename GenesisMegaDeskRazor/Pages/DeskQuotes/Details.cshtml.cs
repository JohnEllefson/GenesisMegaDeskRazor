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
    public class DetailsModel : PageModel
    {
        private readonly GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext _context;

        public DetailsModel(GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext context)
        {
            _context = context;
        }

        public DeskQuote DeskQuote { get; set; } = default!;
        public QuoteRowData DetailsData { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            QuoteRowData Data = new QuoteRowData();


            var DetailQuery = (from desk in _context.Desk
                               where desk.Id == id
                               join deskQuote in _context.DeskQuote
                               on desk.Id equals deskQuote.Id
                               select new { DeskTable = desk, QuoteTable = deskQuote }).ToList();

            // Set all data from query so it can later be displayed
            Data.CustomerName = DetailQuery[0].DeskTable.Name;
            Data.Date = DetailQuery[0].QuoteTable.Date;
            Data.RushOrderDays = DetailQuery[0].DeskTable.RushOrderDays;
            Data.DeskMaterial = DetailQuery[0].DeskTable.Material;
            Data.DrawersNum = DetailQuery[0].DeskTable.NumberOfDrawers;
            Data.Width = DetailQuery[0].DeskTable.Width;
            Data.Depth = DetailQuery[0].DeskTable.Depth;
            Data.TotalPrice = DetailQuery[0].QuoteTable.TotalPrice;
            DetailsData = Data;


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
    }
}
