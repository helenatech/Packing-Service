using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PackingService.API.Data;
using PackingService.API.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PackingService API",
        Version = "v1",
        Description = "API para empacotamento de produtos em caixas"
    });
});

builder.Services.AddScoped<ProdutoNaCaixaService>();
builder.Services.AddScoped<EscolhaEmpacotamentoService>();
builder.Services.AddScoped<EmpacotamentoService>();
builder.Services.AddDbContext<PackingDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PackingService API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

