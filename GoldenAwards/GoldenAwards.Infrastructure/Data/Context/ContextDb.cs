using GoldenAwards.Domain.Models.Movies;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GoldenAwards.Infrastructure.Data.Context
{
    public class ContextDb : BaseDbContext<ContextDb>
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdnMapping).Assembly);
        }
    }
}
