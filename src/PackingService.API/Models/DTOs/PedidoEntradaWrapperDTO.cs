using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PackingService.API.Models.DTOs
{
    public class PedidoEntradaWrapperDTO
    {
        [Required]
        [MinLength(1)]
        [JsonPropertyName("pedidos")]
        public List<PedidoEntradaDTO> Pedidos { get; set; } = new();
    }
}
