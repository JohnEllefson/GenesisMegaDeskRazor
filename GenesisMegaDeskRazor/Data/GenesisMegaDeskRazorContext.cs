using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GenesisMegaDeskRazor.Models;

namespace GenesisMegaDeskRazor.Data
{
    public class GenesisMegaDeskRazorContext : DbContext
    {
        public GenesisMegaDeskRazorContext (DbContextOptions<GenesisMegaDeskRazorContext> options)
            : base(options)
        {
        }

        public DbSet<GenesisMegaDeskRazor.Models.DeskQuote> DeskQuote { get; set; } = default!;
    }
}
