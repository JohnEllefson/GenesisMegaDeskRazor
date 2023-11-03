using GenesisMegaDeskRazor.Data;
using Microsoft.EntityFrameworkCore;

namespace GenesisMegaDeskRazor.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GenesisMegaDeskRazorContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<GenesisMegaDeskRazorContext>>()))
            {
                if (context == null || context.DeskQuote == null || context.Desk == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any quote.
                if (context.DeskQuote.Any())
                {
                    return;   // DB has been seeded
                }

                //context.DeskQuote.AddRange(
                //    new DeskQuote
                //    {
                //        Id = 1,
                //        DeskId = 1,
                //        Date = DateTime.Parse("2022-5-26"),
                //        TotalPrice = 150
                //    },

                //    new DeskQuote
                //    {
                //        Id = 2,
                //        DeskId = 2,
                //        Date = DateTime.Parse("2023-1-17"),
                //        TotalPrice = 150
                //    },

                //    new DeskQuote
                //    {
                //        Id = 3,
                //        DeskId = 3,
                //        Date = DateTime.Parse("2023-6-8"),
                //        TotalPrice = 150
                //    },

                //    new DeskQuote
                //    {
                //        Id = 4,
                //        DeskId = 4,
                //        Date = DateTime.Parse("2023-4-18"),
                //        TotalPrice = 150
                //    }
                //);

                //context.Desk.AddRange(
                //    new Desk
                //    {
                //        Id = 1,
                //        Name = "Harry",
                //        Width = 30,
                //        Depth = 28,
                //        NumberOfDrawers = 3,
                //        Material = Desk.DesktopMaterial.Rosewood,
                //        RushOrderDays = 7
                //    },

                //    new Desk
                //    {
                //        Id = 2,
                //        Name = "Jeffery",
                //        Width = 43,
                //        Depth = 35,
                //        NumberOfDrawers = 1,
                //        Material = Desk.DesktopMaterial.Veneer,
                //        RushOrderDays = 14
                //    },

                //    new Desk
                //    {
                //        Id = 3,
                //        Name = "Susan",
                //        Width = 35,
                //        Depth = 45,
                //        NumberOfDrawers = 5,
                //        Material = Desk.DesktopMaterial.Pine,
                //        RushOrderDays = 5
                //    },

                //    new Desk
                //    {
                //        Id = 4,
                //        Name = "Jeffery",
                //        Width = 26,
                //        Depth = 20,
                //        NumberOfDrawers = 4,
                //        Material = Desk.DesktopMaterial.Oak,
                //        RushOrderDays = 14
                //    }
                //);
                //context.SaveChanges();
            }
        }
    }
}
