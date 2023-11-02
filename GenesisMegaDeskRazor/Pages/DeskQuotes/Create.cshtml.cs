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

            DeskQuote.DeskId = Desk.Id;
            DeskQuote.Date = DateTime.Today;
            DeskQuote.AdditionalSqInchCost = CalcAdditionalSqInchCost();
            DeskQuote.DrawerCost = CalcDrawerCost();
            DeskQuote.SurfaceMaterialCost = CalcSurfaceMaterialCost();
            DeskQuote.RushOrderCost = CalcRushOrderCost();
            DeskQuote.TotalPrice = CalcTotalPrice();

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.DeskQuote == null || DeskQuote == null || Desk == null || _context.Desk == null)
            {
                return Page();
            }

            _context.DeskQuote.Add(DeskQuote);
            _context.Desk.Add(Desk);

       
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("DeskData");

            return RedirectToPage("./Index");
        }


        // Calculate the cost of adding more desk area
        private double CalcAdditionalSqInchCost()
        {
            double sqInchCost = 0;
            double pricePerExtraInch = 1;
            double squareInches = Desk.Width * Desk.Depth;

            if (squareInches > 1000)
            {
                sqInchCost = pricePerExtraInch * (squareInches - 1000);
            }

            return sqInchCost;
        }

        // Calculate the added cost of drawers
        private double CalcDrawerCost()
        {
            double costPerDrawer = 50;

            return costPerDrawer * Desk.NumberOfDrawers;
        }

        // Get the cost of the desktop material already contained in the DesktopMaterials enum values
        private double CalcSurfaceMaterialCost()
        {
            return (double)Material;
        }

        private double CalcRushOrderCost()
        {
            double sqInch = Desk.Width * Desk.Depth;
            int rush = Desk.RushOrderDays;

            // If rush days are 14, then there is no rush or added cost.
            if (rush == 14)
            {
                return 0;
            }

            // Call GetRushOrder to populate rushOrderPrices array
            GetRushOrder();

            // Headers help to easily find the coordinates of the priceChart
            int[] rushOrderHeader = { 3, 5, 7 };
            int[] deskSizeHeader = { 1000, 2000, 4610 }; //4608 is the max area given the maximum values of width and depth.

            // Find the index corresponding to sqInch is less than the value.
            int indexSize = Array.FindIndex(deskSizeHeader, header => sqInch < header);

            // Find the index that matches the value of rush days
            int indexRush = Array.IndexOf(rushOrderHeader, rush);

            // Check if rushOrderPrices is null, indicating an error in reading the file
            if (DeskQuote.rushOrderPrices == null)
            {
                // Handle the error, for example, by returning a default value or showing an error message.
                Console.WriteLine("Error: Unable to retrieve rush order prices.");
                return 0; // Default value, or handle the error as needed.
            }

            // Calculate the rush order cost using rushOrderPrices array
            double rushOrderCost = DeskQuote.rushOrderPrices[indexRush, indexSize];

            return rushOrderCost;
        }

        // New method to handle rush order prices
        public void GetRushOrder()
        {
            try
            {
                string filePath = "rushOrderPrices.txt";
                string[] lines = System.IO.File.ReadAllLines(filePath);

                // Initialize the existing class-level 2D array with three rows and three columns
                DeskQuote.rushOrderPrices = new double[3, 3];

                int row = 0;
                int col = 0;

                foreach (string line in lines)
                {
                    // Parse the price from each line and add it to the array
                    if (row < 3 && col < 3)
                    {
                        if (double.TryParse(line, out double price))
                        {
                            DeskQuote.rushOrderPrices[row, col] = price;
                            col++;
                        }
                    }

                    if (col >= 3)
                    {
                        col = 0;
                        row++;
                    }

                    if (row >= 3)
                    {
                        break; // We have filled the entire 3x3 array
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading rush order prices: {ex.Message}");
            }
        }


        private double CalcTotalPrice()
        {
            return DeskQuote.BaseDeskPrice + DeskQuote.AdditionalSqInchCost + DeskQuote.DrawerCost + DeskQuote.SurfaceMaterialCost + DeskQuote.RushOrderCost;
        }
    }
}


