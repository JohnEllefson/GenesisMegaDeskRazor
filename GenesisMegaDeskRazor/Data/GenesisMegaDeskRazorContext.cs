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

        public DbSet<GenesisMegaDeskRazor.Models.Desk> Desk { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the primary key for the Desk entity
            modelBuilder.Entity<Desk>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();

            // Configure the primary key for the DeskQuote entity
            modelBuilder.Entity<DeskQuote>()
                .Property(dq => dq.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
