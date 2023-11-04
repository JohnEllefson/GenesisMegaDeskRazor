using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GenesisMegaDeskRazor.Data;
using GenesisMegaDeskRazor.Models;
using static GenesisMegaDeskRazor.Models.Desk;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Reflection.Metadata.BlobBuilder;
using NuGet.Versioning;

namespace GenesisMegaDeskRazor.Pages.DeskQuotes
{
    public class QuoteRowData
    {
        public int DeskQuoteId { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public int RushOrderDays { get; set; }
        public DesktopMaterial DeskMaterial { get; set; }
        public int DrawersNum { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public double TotalPrice { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext _context;

        public IndexModel(GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; } = default!;
        public IList<QuoteRowData> QuoteDataRows { get;set; } = new List<QuoteRowData>();


        // For searching quotes by customer name
        [BindProperty(SupportsGet = true)]
        public string? NameSearchString { get; set; }


        // For sorting quotes by date or customer name
        public SelectList? SortOptions { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOption { get; set; }


        public async Task OnGetAsync()
        {
            var orderedData = (from desk in _context.Desk
                               join deskQuote in _context.DeskQuote
                               on desk.Id equals deskQuote.DeskId
                               select new { DeskTable = desk, QuoteTable = deskQuote }).ToList();

            SortOptions = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Name", Value = "Name" },
                new SelectListItem { Text = "Date", Value = "Date" }
            }, "Value", "Text");

            // Create a LINQ string that combines the Desk and DeskQuote tables by the
            // id value and then sorts and searches according to whats been selected.
            if (!string.IsNullOrEmpty(NameSearchString))
            {
                if (SortOption == "Name")
                {
                    orderedData = (from desk in _context.Desk
                                   where desk.Name.Contains(NameSearchString)
                                   join deskQuote in _context.DeskQuote
                                   on desk.Id equals deskQuote.DeskId
                                   orderby desk.Name
                                   select new { DeskTable = desk, QuoteTable = deskQuote }).ToList();
                }
                else if (SortOption == "Date")
                {
                    orderedData = (from desk in _context.Desk
                                   where desk.Name.Contains(NameSearchString)
                                   join deskQuote in _context.DeskQuote
                                   on desk.Id equals deskQuote.DeskId
                                   orderby deskQuote.Date
                                   select new { DeskTable = desk, QuoteTable = deskQuote }).ToList();
                }
                else  // SortOption == "None"
                {
                    orderedData = (from desk in _context.Desk
                                   where desk.Name.Contains(NameSearchString)
                                   join deskQuote in _context.DeskQuote
                                   on desk.Id equals deskQuote.DeskId
                                   select new { DeskTable = desk, QuoteTable = deskQuote }).ToList();
                }
            }
            else // No search string entered
            {
                if (SortOption == "Name")
                {
                    orderedData = (from desk in _context.Desk
                                   join deskQuote in _context.DeskQuote
                                   on desk.Id equals deskQuote.DeskId
                                   orderby desk.Name
                                   select new { DeskTable = desk, QuoteTable = deskQuote }).ToList();
                }
                else if (SortOption == "Date")
                {
                    orderedData = (from desk in _context.Desk
                                   join deskQuote in _context.DeskQuote
                                   on desk.Id equals deskQuote.DeskId
                                   orderby deskQuote.Date
                                   select new { DeskTable = desk, QuoteTable = deskQuote }).ToList();
                }
                else  // SortOption == "None"
                {
                    orderedData = (from desk in _context.Desk
                                   join deskQuote in _context.DeskQuote
                                   on desk.Id equals deskQuote.DeskId
                                   select new { DeskTable = desk, QuoteTable = deskQuote }).ToList();
                }
            }

            // Save data from DeskQuote and Desk into a list to later be displayed in a table
            foreach (var quote in orderedData)
            {
                QuoteRowData rowData = new QuoteRowData();
                rowData.Date = quote.QuoteTable.Date;
                rowData.TotalPrice = quote.QuoteTable.TotalPrice;
                rowData.DeskQuoteId = quote.QuoteTable.DeskId;

                rowData.CustomerName = quote.DeskTable.Name;
                rowData.RushOrderDays = quote.DeskTable.RushOrderDays;
                rowData.DeskMaterial = quote.DeskTable.Material;
                rowData.DrawersNum = quote.DeskTable.NumberOfDrawers;
                rowData.Width = quote.DeskTable.Width;
                rowData.Depth = quote.DeskTable.Depth;

                // Add the given quote's data to be displayed to the list 
                QuoteDataRows.Add(rowData);
            }

            if (_context.DeskQuote != null)
            {
                DeskQuote = await _context.DeskQuote.ToListAsync();
            }
        }
    }
}
