using PackingService.API.Models.DTOs;
using PackingService.API.Models.Entidades;

namespace PackingService.API.Services
{
    public class EscolhaEmpacotamentoService
    {
        private readonly ProdutoNaCaixaService _produtoEmCaixaService;

        public EscolhaEmpacotamentoService(ProdutoNaCaixaService produtoEmCaixaService)
        {
            _produtoEmCaixaService = produtoEmCaixaService;
        }

        public List<PedidoCaixaSaidaDTO> Empacotar(PedidoEntradaDTO pedido, List<Caixa> caixasDisponiveis)
        {
            var caixasUsadas = new List<PedidoCaixaSaidaDTO>();
            var produtosRestantes = new List<ProdutoEntradaDTO>(pedido.Produtos);

            // Ordena caixas por volume crescente
            var caixasOrdenadas = caixasDisponiveis.OrderBy(c => c.Dimensoes.Volume).ToList();

            while (produtosRestantes.Any())
            {
                bool agrupamentoEncontrado = false;

                // Tentar achar grupos maiores (pares, trios...) que caibam juntos numa caixa
                // Vamos tentar pares e depois individuais

                // Tentativa 1: pares
                for (int i = 0; i < produtosRestantes.Count && !agrupamentoEncontrado; i++)
                {
                    for (int j = i + 1; j < produtosRestantes.Count && !agrupamentoEncontrado; j++)
                    {
                        var grupoTeste = new List<ProdutoEntradaDTO> { produtosRestantes[i], produtosRestantes[j] };

                        var caixaParaGrupo = caixasOrdenadas
                            .FirstOrDefault(c => ProdutoCabemNaCaixa(grupoTeste, c.Dimensoes));

                        if (caixaParaGrupo != null)
                        {
                            caixasUsadas.Add(new PedidoCaixaSaidaDTO
                            {
                                CaixaId = caixaParaGrupo.Nome,
                                Produtos = grupoTeste.Select(p => p.ProdutoId).ToList()
                            });

                            produtosRestantes.Remove(produtosRestantes[i]);
                            produtosRestantes.Remove(produtosRestantes[j - 1]); // j-1 pois a lista diminui após remover i
                            agrupamentoEncontrado = true;
                        }
                    }
                }

                if (agrupamentoEncontrado) continue;

                // Tentativa 2: produto individual em caixa que caiba
                var produto = produtosRestantes[0];

                var caixaIndividual = caixasOrdenadas.FirstOrDefault(c =>
                    _produtoEmCaixaService.ProdutoCabeNaCaixa(produto.Dimensoes, c.Dimensoes));

                if (caixaIndividual != null)
                {
                    caixasUsadas.Add(new PedidoCaixaSaidaDTO
                    {
                        CaixaId = caixaIndividual.Nome,
                        Produtos = new List<string> { produto.ProdutoId }
                    });
                }
                else
                {
                    caixasUsadas.Add(new PedidoCaixaSaidaDTO
                    {
                        CaixaId = "null",
                        Produtos = new List<string> { produto.ProdutoId },
                        Observacao = "Produto não cabe em nenhuma caixa disponível."
                    });
                }

                produtosRestantes.RemoveAt(0);
            }

            return caixasUsadas;
        }

        // Método auxiliar para testar se um grupo de produtos cabe numa caixa
        private bool ProdutoCabemNaCaixa(List<ProdutoEntradaDTO> produtos, Dimensoes caixa)
        {
            // Soma volumes
            var somaVolumes = produtos.Sum(p => p.Dimensoes.Volume);

            if (somaVolumes > caixa.Volume) return false;

            // Verifica se cada produto cabe nas dimensões da caixa (rotação permitida)
            if (produtos.Any(p => !_produtoEmCaixaService.ProdutoCabeNaCaixa(p.Dimensoes, caixa)))
                return false;

            return true;
        }
    }
}
