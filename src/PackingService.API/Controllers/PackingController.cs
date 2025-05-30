using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackingService.API.Data;
using PackingService.API.Models.DTOs;
using PackingService.API.Services;

namespace PackingService.API.Controllers
{
    [ApiController]
    [Route("v1/empacotarPedidos")]
    public class PackingController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<List<PedidoRespostaDTO>>> EmpacotarPedidos(
            [FromBody] List<PedidoEntradaDTO> pedidos,
            [FromServices] PackingDataContext _context,
            [FromServices] EscolhaEmpacotamentoService _empacotamentoService)
        {
            var caixas = await _context.Caixas
                .AsNoTracking()
                .ToListAsync();

            if (caixas == null || caixas.Count == 0)
                return BadRequest("Nenhuma caixa disponível.");

            var resultado = new List<PedidoRespostaDTO>();

            foreach (var pedido in pedidos)
            {
                var caixasUsadas = _empacotamentoService.Empacotar(pedido, caixas);

                resultado.Add(new PedidoRespostaDTO
                {
                    PedidoId = pedido.PedidoId,
                    Caixas = caixasUsadas
                });
            }

            return Ok(new PedidoSaidaWrapperDTO { Pedidos = resultado });
        }
    }
}
