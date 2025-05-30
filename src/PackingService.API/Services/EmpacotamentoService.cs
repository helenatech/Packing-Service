using Microsoft.EntityFrameworkCore;
using PackingService.API.Data;
using PackingService.API.Models.DTOs;
using PackingService.API.Models.Entidades;
using System;

namespace PackingService.API.Services
{
    public class EmpacotamentoService
    {
        private readonly PackingDataContext _context;
        private readonly EscolhaEmpacotamentoService _estrategiaEmpacotamento;

        public EmpacotamentoService(PackingDataContext context, EscolhaEmpacotamentoService estrategiaEmpacotamento)
        {
            _context = context;
            _estrategiaEmpacotamento = estrategiaEmpacotamento;
        }

        public async Task<List<PedidoRespostaDTO>> EmpacotarPedidosAsync(List<PedidoEntradaDTO> pedidos)
        {
            var caixas = await _context.Caixas.AsNoTracking().ToListAsync();
            var pedidosEmpacotados = new List<PedidoRespostaDTO>();

            foreach (var pedido in pedidos)
            {
                var caixasUsadas = _estrategiaEmpacotamento.Empacotar(pedido, caixas);

                pedidosEmpacotados.Add(new PedidoRespostaDTO
                {
                    PedidoId = pedido.PedidoId,
                    Caixas = caixasUsadas
                });
            }

            return pedidosEmpacotados;
        }
    }
}

