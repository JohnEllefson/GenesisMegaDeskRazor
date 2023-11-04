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
using Microsoft.CodeAnalysis;

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
 
        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.TryGetValue("DeskData", out byte[] data))
            {
                string deskData = HttpContext.Session.GetString("DeskData");
                Desk = JsonConvert.DeserializeObject<Desk>(deskData);
            }

            return Page();
        }

                   
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid || _context.Desk == null || Desk == null)
            {
                return Page();
            }

            //var deskId = DeskId;
            if (ValidateDesk(Desk))
            {
                HttpContext.Session.SetString("DeskData", JsonConvert.SerializeObject(Desk));

                return RedirectToPage("../DeskQuotes/Create");
            }
            else
            {
                return Page();
            }
        }

        public bool ValidateDesk(Desk ThisDesk)
        {
            bool width = true;
            bool depth = true;
            if (ThisDesk.Width < Desk.MinWidth || ThisDesk.Width > Desk.MaxWidth)
            {
                TempData["Message"] = $"Desk width must be between {Desk.MinWidth} and {Desk.MaxWidth} inches.";
                width = false;
            }
            if (ThisDesk.Depth < Desk.MinDepth || ThisDesk.Depth > Desk.MaxDepth)
            {
                TempData["Message"] = $"Desk depth must be between {Desk.MinDepth} and {Desk.MaxDepth} inches.";
                depth = false;
            }

            return width & depth;
        }
    }
}
