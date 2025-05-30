using PackingService.API.Models.Entidades;
using PackingService.API.Services;

namespace PackingService.Tests;

public class ProdutoNaCaixaServiceTests
{
    private readonly ProdutoNaCaixaService _service = new();

    [Theory] //teste parametrizado
    [InlineData(40, 10, 25, 80, 50, 40, true)] //dimensões Produto e Caixa (usando tamanho da 02)
    [InlineData(120, 60, 70, 80, 50, 40, false)] // Produto não cabe
    public void ProdutoCabeNaCaixa_DeveRetornarResultadoCorreto(double pAlt, double pLar, double pComp, double cAlt, double cLar, double cComp, bool esperado)
    {
        var produto = new Dimensoes { Altura = pAlt, Largura = pLar, Comprimento = pComp };
        var caixa = new Dimensoes { Altura = cAlt, Largura = cLar, Comprimento = cComp };

        var resultado = _service.ProdutoCabeNaCaixa(produto, caixa);

        Assert.Equal(esperado, resultado);
    }
}
