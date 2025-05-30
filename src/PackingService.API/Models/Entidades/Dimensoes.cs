namespace PackingService.API.Models.Entidades
{
    public class Dimensoes
    {
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Volume => Altura * Largura * Comprimento;
    }
}
