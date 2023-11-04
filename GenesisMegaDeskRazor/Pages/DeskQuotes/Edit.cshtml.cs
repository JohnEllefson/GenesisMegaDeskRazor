using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GenesisMegaDeskRazor.Data;
using GenesisMegaDeskRazor.Models;
using Newtonsoft.Json;

namespace GenesisMegaDeskRazor.Pages.DeskQuotes
{
    public class EditModel : PageModel
    {
        private readonly GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext _context;

        public EditModel(GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; } = default!;
        [BindProperty]
        public QuoteRowData DetailsData { get; set; } = new QuoteRowData();
        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            QuoteRowData Data = new QuoteRowData();

            var DetailQuery = (from desk in _context.Desk
                               where desk.Id == id
                               join deskQuote in _context.DeskQuote
                               on desk.Id equals deskQuote.Id
                               select new { DeskTable = desk, QuoteTable = deskQuote }).ToList();

            // Set all data from query so it can later be displayed
            Data.DeskQuoteId = DetailQuery[0].QuoteTable.DeskId;
            Data.CustomerName = DetailQuery[0].DeskTable.Name;
            Data.Date = DetailQuery[0].QuoteTable.Date;
            Data.RushOrderDays = DetailQuery[0].DeskTable.RushOrderDays;
            Data.DeskMaterial = DetailQuery[0].DeskTable.Material;
            Data.DrawersNum = DetailQuery[0].DeskTable.NumberOfDrawers;
            Data.Width = DetailQuery[0].DeskTable.Width;
            Data.Depth = DetailQuery[0].DeskTable.Depth;
            Data.TotalPrice = DetailQuery[0].QuoteTable.TotalPrice;
            DetailsData = Data;
            
            // Save the session state so that the data persists between the OnGetAsync() and OnPostAsync()
            HttpContext.Session.SetString("EditData", JsonConvert.SerializeObject(DetailsData));

            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.DeskQuote == null || DeskQuote == null || _context.Desk == null)
            {
                return Page();
            }

            //var deskId = DeskId;
            if (ValidateData(DetailsData))
            {
                if (DeskQuote.Id == null || _context.DeskQuote == null)
                {
                    return NotFound();
                }

                var Quote = await _context.DeskQuote.FirstOrDefaultAsync(m => m.Id == DeskQuote.Id);
                var Desk = await _context.Desk.FirstOrDefaultAsync(m => m.Id == DeskQuote.Id);
                DeskQuote NewQuote = new DeskQuote();
                Desk NewDesk = new Desk();

                if (Quote == null || Desk == null)
                {
                    return NotFound();
                }

                // Set all properties for the new Desk
                NewDesk.Id = DetailsData.DeskQuoteId;
                NewDesk.Name = DetailsData.CustomerName;
                NewDesk.RushOrderDays = DetailsData.RushOrderDays;
                NewDesk.Material = DetailsData.DeskMaterial;
                NewDesk.NumberOfDrawers = DetailsData.DrawersNum;
                NewDesk.Width = DetailsData.Width;
                NewDesk.Depth = DetailsData.Depth;

                // Calculate the updated prices
                Quote.CalcTotalPrice(NewDesk);

                // Set all properties for the new DeskQuote
                NewQuote.Id = DetailsData.DeskQuoteId;
                NewQuote.DeskId = DetailsData.DeskQuoteId;
                NewQuote.Date = DetailsData.Date;
                NewQuote.BaseDeskPrice = Quote.BaseDeskPrice;
                NewQuote.rushOrderPrices = Quote.rushOrderPrices;
                NewQuote.RushOrderCost = Quote.RushOrderCost;
                NewQuote.SurfaceMaterialCost = Quote.SurfaceMaterialCost;
                NewQuote.AdditionalSqInchCost = Quote.AdditionalSqInchCost;
                NewQuote.DrawerCost = Quote.DrawerCost;
                NewQuote.TotalPrice = Quote.TotalPrice;

                // Remove the old DeskQuote and Desk records
                _context.DeskQuote.Remove(Quote);
                _context.Desk.Remove(Desk);

                // Add new DeskQuote and Desk records to replace the previous
                _context.DeskQuote.Add(NewQuote);
                _context.Desk.Add(NewDesk);

                await _context.SaveChangesAsync();
                return RedirectToPage("../DeskQuotes/Index");
            }
            else
            {
                return Page();
            }
        }

        public bool ValidateData(QuoteRowData DetailsData)
        {
            bool width = true;
            bool depth = true;
            if (DetailsData.Width < Desk.MinWidth || DetailsData.Width > Desk.MaxWidth)
            {
                TempData["Message"] = $"Desk width must be between {Desk.MinWidth} and {Desk.MaxWidth} inches.";
                width = false;
            }
            if (DetailsData.Depth < Desk.MinDepth || DetailsData.Depth > Desk.MaxDepth)
            {
                TempData["Message"] = $"Desk depth must be between {Desk.MinDepth} and {Desk.MaxDepth} inches.";
                depth = false;
            }

            return width & depth;
        }







        private bool DeskQuoteExists(int id)
        {
          return (_context.DeskQuote?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public string DetermineInitialWood()
        {
            string imageURL;

            switch (DetailsData.DeskMaterial)
            {
                case Desk.DesktopMaterial.Laminate:
                    imageURL = "/Images/Laminate.jpg";
                    break;
                case Desk.DesktopMaterial.Oak:
                    imageURL = "/Images/Oak.jpg";
                    break;
                case Desk.DesktopMaterial.Rosewood:
                    imageURL = "/Images/Rosewood.jpg";
                    break;
                case Desk.DesktopMaterial.Veneer:
                    imageURL = "/Images/Veneer.jpg";
                    break;
                default: // DesktopMaterial.Pine
                    imageURL = "/Images/Pine.jpg";
                    break;
            }

            return imageURL;
        }
    }
}
