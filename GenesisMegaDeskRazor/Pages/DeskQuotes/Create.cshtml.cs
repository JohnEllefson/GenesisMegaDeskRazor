using GenesisMegaDeskRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GenesisMegaDeskRazor.Pages.DeskQuotes
{
    public class CreateModel : PageModel
    {
        private readonly GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext _context;

        public CreateModel(GenesisMegaDeskRazor.Data.GenesisMegaDeskRazorContext context)
        {
            _context = context;
        }

        public Desk Desk { get; set; } = new Desk();
        public Desk.DesktopMaterial Material { get; set; }
        [BindProperty]
        public DeskQuote DeskQuote { get; set; } = new DeskQuote();

        public async Task<IActionResult> OnGetAsync()
        {

            string deskData = HttpContext.Session.GetString("DeskData");
            Desk = JsonConvert.DeserializeObject<Desk>(deskData);

            Material = Desk.Material;

            // DeskQuote.DeskId = Desk.Id;
            DeskQuote.Date = DateTime.Today;
            DeskQuote.CalcTotalPrice(Desk);
           // DeskQuote.AdditionalSqInchCost = CalcAdditionalSqInchCost();
            //DeskQuote.DrawerCost = CalcDrawerCost();
            //DeskQuote.SurfaceMaterialCost = CalcSurfaceMaterialCost();
            //DeskQuote.RushOrderCost = CalcRushOrderCost();
            //DeskQuote.TotalPrice = CalcTotalPrice();

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.DeskQuote == null || DeskQuote == null || Desk == null || _context.Desk == null)
            {
                return Page();
            }

            string deskData = HttpContext.Session.GetString("DeskData");
            Desk = JsonConvert.DeserializeObject<Desk>(deskData);

            _context.Desk.Add(Desk);
            await _context.SaveChangesAsync();
            int deskId = Desk.Id;

            DeskQuote.DeskId = deskId;    

            _context.DeskQuote.Add(DeskQuote);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("DeskData");

            return RedirectToPage("./Index");
        }
    }
}


