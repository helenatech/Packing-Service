using PackingService.API.Models.DTOs;
using PackingService.API.Models.Entidades;
using PackingService.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingService.Tests
{
    public class EscolhaEmpacotamentoServiceTests
    {
        private readonly ProdutoNaCaixaService _produtoService = new();
        private readonly EscolhaEmpacotamentoService _empacotamentoService;

        public EscolhaEmpacotamentoServiceTests()
        {
            _empacotamentoService = new EscolhaEmpacotamentoService(_produtoService);
        }

        [Fact]
        public void Empacotar_DeveRetornarCaixaUnicaQuandoProdutosCabem()
        {
            var pedido = new PedidoEntradaDTO
            {
                PedidoId = 1,
                Produtos = new List<ProdutoEntradaDTO>
            {
                new ProdutoEntradaDTO
                {
                    ProdutoId = "PS5",
                    Dimensoes = new Dimensoes { Altura = 40, Largura = 10, Comprimento = 25 }
                },
                new ProdutoEntradaDTO
                {
                    ProdutoId = "Volante",
                    Dimensoes = new Dimensoes { Altura = 40, Largura = 30, Comprimento = 30 }
                }
            }
            };

            var caixasDisponiveis = new List<Caixa>
        {
            new Caixa
            {
                Nome = "Caixa 2",
                Dimensoes = new Dimensoes { Altura = 80, Largura = 50, Comprimento = 40 }
            },
            new Caixa
            {
                Nome = "Caixa 1",
                Dimensoes = new Dimensoes { Altura = 30, Largura = 40, Comprimento = 80 }
            }
        };

            var resultado = _empacotamentoService.Empacotar(pedido, caixasDisponiveis);

            Assert.Single(resultado);
            Assert.Equal("Caixa 2", resultado[0].CaixaId);
            Assert.Contains("PS5", resultado[0].Produtos);
            Assert.Contains("Volante", resultado[0].Produtos);
        }

    }
}
