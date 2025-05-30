using Microsoft.EntityFrameworkCore;
using PackingService.API.Data.Mappings;
using PackingService.API.Models.Entidades;

namespace PackingService.API.Data
{
    public class PackingDataContext : DbContext
    {
        public DbSet<Caixa> Caixas { get; set; }

        public PackingDataContext(DbContextOptions<PackingDataContext> options)
          : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CaixaMap());
        }
    }
}
