using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PackingService.API.Models.DTOs
{
    public class PedidoEntradaDTO
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }

        [Required]
        [MinLength(1)]
        [JsonPropertyName("produtos")]
        public List<ProdutoEntradaDTO> Produtos { get; set; } = new();
    }
}
