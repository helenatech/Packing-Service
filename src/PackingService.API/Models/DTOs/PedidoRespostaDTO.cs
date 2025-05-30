using System.Text.Json.Serialization;

namespace PackingService.API.Models.DTOs
{
    public class PedidoRespostaDTO
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }

        [JsonPropertyName("caixas")]
        public List<PedidoCaixaSaidaDTO>? Caixas { get; set; } 
    }
}
