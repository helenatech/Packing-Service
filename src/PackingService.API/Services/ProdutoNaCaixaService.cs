using PackingService.API.Models.Entidades;

namespace PackingService.API.Services
{
    public class ProdutoNaCaixaService
    {
        public bool ProdutoCabeNaCaixa(Dimensoes produto, Dimensoes caixa)
        {
            double[] pDims = new[] { produto.Altura, produto.Largura, produto.Comprimento }.OrderBy(x => x).ToArray();
            double[] cDims = new[] { caixa.Altura, caixa.Largura, caixa.Comprimento }.OrderBy(x => x).ToArray();

            return pDims[0] <= cDims[0] && pDims[1] <= cDims[1] && pDims[2] <= cDims[2];
        }
    }
}
