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

            // Ordena caixas da menor para maior
            var caixasOrdenadas = caixasDisponiveis.OrderBy(c => c.Dimensoes.Volume).ToList();

            while (produtosRestantes.Any())
            {
                bool agrupamentoEncontrado = false;
                
                // Encontrar pares que caibam juntos na mesma caixa
                for (int i = 0; i < produtosRestantes.Count && !agrupamentoEncontrado; i++)
                {
                    for (int j = i + 1; j < produtosRestantes.Count && !agrupamentoEncontrado; j++) //tenta combinar com outro produto, procurando após a posição de i
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
                            produtosRestantes.Remove(produtosRestantes[j - 1]); //serve pra remover o produto j também, descendo uma casinha no índice, já que o i foi removido
                            agrupamentoEncontrado = true; //interrompe o laço
                        }
                    }
                }

                if (agrupamentoEncontrado) continue;

                // Pegar o produto individual que sobrou
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

        // método auxiliar de produtoCabeNaCaixa
        private bool ProdutoCabemNaCaixa(List<ProdutoEntradaDTO> produtos, Dimensoes caixa)
        {
            // soma volumes
            var somaVolumes = produtos.Sum(p => p.Dimensoes.Volume);

            if (somaVolumes > caixa.Volume) return false;

            // Verifica se cada produto cabe nas dimensões da caixa, sendo que podemos rotacionar o produto pra caber
            if (produtos.Any(p => !_produtoEmCaixaService.ProdutoCabeNaCaixa(p.Dimensoes, caixa)))
                return false;

            return true;
        }
    }
}
