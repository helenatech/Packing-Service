using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackingService.API.Models.Entidades;

namespace PackingService.API.Data.Mappings
{
    public class CaixaMap : IEntityTypeConfiguration<Caixa>
    {
        public void Configure(EntityTypeBuilder<Caixa> builder)
        {
            builder.ToTable("Caixa");

            builder.HasKey(x => x.CaixaId);

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.OwnsOne(c => c.Dimensoes, dim =>
            {
                dim.Property(d => d.Altura).HasColumnName("Altura");
                dim.Property(d => d.Largura).HasColumnName("Largura");
                dim.Property(d => d.Comprimento).HasColumnName("Comprimento");

                dim.HasData(
                    new { CaixaId = 1, Altura = 30.0, Largura = 40.0, Comprimento = 80.0 },
                    new { CaixaId = 2, Altura = 80.0, Largura = 50.0, Comprimento = 40.0 },
                    new { CaixaId = 3, Altura = 50.0, Largura = 80.0, Comprimento = 60.0 }
                );
            });

            builder.HasData(
                new Caixa { CaixaId = 1, Nome = "Caixa 01" },
                new Caixa { CaixaId = 2, Nome = "Caixa 02" },
                new Caixa { CaixaId = 3, Nome = "Caixa 03" }
            );
        }
    }
}
