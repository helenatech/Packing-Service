using PackingService.API.Models.Entidades;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PackingService.API.Models.DTOs
{
    public class ProdutoEntradaDTO
    {
        [Required]
        [JsonPropertyName("produto_id")]
        public string ProdutoId { get; set; } = null!;
     
        [Required]
        [JsonPropertyName("dimensoes")]
        public Dimensoes Dimensoes { get; set; } = new();
    }
}
