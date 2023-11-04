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

                //context.Desk.ExecuteSqlCommand("SET IDENTITY_INSERT YourTable ON");

                context.DeskQuote.AddRange(
                    new DeskQuote
                    {
                        DeskId = 1,
                        Date = DateTime.Parse("2022-5-26"),
                        BaseDeskPrice = 200,
                        AdditionalSqInchCost = 0,
                        DrawerCost = 150,
                        SurfaceMaterialCost = 300,
                        RushOrderCost = 30,
                        TotalPrice = 680
                    },

                    new DeskQuote
                    {
                        DeskId = 2,
                        Date = DateTime.Parse("2023-1-17"),
                        BaseDeskPrice = 200,
                        AdditionalSqInchCost = 505,
                        DrawerCost = 50,
                        SurfaceMaterialCost = 125,
                        RushOrderCost = 0,
                        TotalPrice = 880
                    },

                    new DeskQuote
                    {
                        DeskId = 3,
                        Date = DateTime.Parse("2023-6-8"),
                        BaseDeskPrice = 200,
                        AdditionalSqInchCost = 2375,
                        DrawerCost = 250,
                        SurfaceMaterialCost = 50,
                        RushOrderCost = 60,
                        TotalPrice = 2935
                    },

                    new DeskQuote
                    {
                        DeskId = 4,
                        Date = DateTime.Parse("2023-4-18"),
                        BaseDeskPrice = 200,
                        AdditionalSqInchCost = 152,
                        DrawerCost = 200,
                        SurfaceMaterialCost = 200,
                        RushOrderCost = 0,
                        TotalPrice = 752
                    },

                    new DeskQuote
                    {
                        DeskId = 5,
                        Date = DateTime.Parse("2022-12-13"),
                        BaseDeskPrice = 200,
                        AdditionalSqInchCost = 1622,
                        DrawerCost = 300,
                        SurfaceMaterialCost = 300,
                        RushOrderCost = 60,
                        TotalPrice = 2482
                    },

                    new DeskQuote
                    {
                        DeskId = 6,
                        Date = DateTime.Parse("2023-4-20"),
                        BaseDeskPrice = 200,
                        AdditionalSqInchCost = 1312,
                        DrawerCost = 350,
                        SurfaceMaterialCost = 200,
                        RushOrderCost = 40,
                        TotalPrice = 2102
                    },

                    new DeskQuote
                    {
                        DeskId = 7,
                        Date = DateTime.Parse("2023-7-16"),
                        BaseDeskPrice = 200,
                        AdditionalSqInchCost = 740,
                        DrawerCost = 100,
                        SurfaceMaterialCost = 125,
                        RushOrderCost = 70,
                        TotalPrice = 1235
                    }
                );

                context.Desk.AddRange(
                    new Desk
                    {
                        Name = "Harry Styles",
                        Width = 30,
                        Depth = 28,
                        NumberOfDrawers = 3,
                        Material = Desk.DesktopMaterial.Rosewood,
                        RushOrderDays = 7
                    },

                    new Desk
                    {
                        Name = "Jeffery Albertson",
                        Width = 43,
                        Depth = 35,
                        NumberOfDrawers = 1,
                        Material = Desk.DesktopMaterial.Veneer,
                        RushOrderDays = 14
                    },

                    new Desk
                    {
                        Name = "Susan Colins",
                        Width = 75,
                        Depth = 45,
                        NumberOfDrawers = 5,
                        Material = Desk.DesktopMaterial.Pine,
                        RushOrderDays = 5
                    },

                    new Desk
                    {
                        Name = "Jeffery Yuler",
                        Width = 96,
                        Depth = 12,
                        NumberOfDrawers = 4,
                        Material = Desk.DesktopMaterial.Oak,
                        RushOrderDays = 14
                    },

                    new Desk
                    {
                        Name = "Dan Davidson",
                        Width = 57,
                        Depth = 46,
                        NumberOfDrawers = 6,
                        Material = Desk.DesktopMaterial.Rosewood,
                        RushOrderDays = 5
                    },

                    new Desk
                    {
                        Name = "Zain Joseffie",
                        Width = 68,
                        Depth = 34,
                        NumberOfDrawers = 7,
                        Material = Desk.DesktopMaterial.Oak,
                        RushOrderDays = 7
                    },

                    new Desk
                    {
                        Name = "Albert Einstein",
                        Width = 58,
                        Depth = 30,
                        NumberOfDrawers = 2,
                        Material = Desk.DesktopMaterial.Veneer,
                        RushOrderDays = 3
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
