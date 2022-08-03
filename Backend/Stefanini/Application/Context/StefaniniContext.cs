using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Context
{
    public class StefaniniContext : DbContext
    {
        public StefaniniContext(DbContextOptions<StefaniniContext> options) : base(options) { }

        public virtual DbSet<Pessoas> Pessoas { get; set; }
        public virtual DbSet<Cidades> Cidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoas>()
                .HasOne(e => e.Cidade)
                .WithMany(c => c.Pessoas)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
