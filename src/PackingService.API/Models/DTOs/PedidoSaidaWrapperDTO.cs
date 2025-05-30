using System.Text.Json.Serialization;

namespace PackingService.API.Models.DTOs
{
    public class PedidoSaidaWrapperDTO
    {
        [JsonPropertyName("pedidos")]
        public List<PedidoRespostaDTO> Pedidos { get; set; } = new();
    }
}
