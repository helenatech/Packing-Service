using System.Text.Json.Serialization;

namespace PackingService.API.Models.DTOs
{
    public class PedidoCaixaSaidaDTO
    {
        [JsonPropertyName("caixa_id")]
        public string CaixaId { get; set; } = null!;

        [JsonPropertyName("produtos")]
        public List<string> Produtos { get; set; } = new();

        [JsonPropertyName("observacao")]
        public string? Observacao { get; set; }
    }
}
