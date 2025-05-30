namespace PackingService.API.Models.Entidades
{
    public class Caixa
    {
        public int CaixaId { get; set; }
        public string Nome { get; set; } = null!;
        public Dimensoes Dimensoes { get; set; } = null!;
    }
}
