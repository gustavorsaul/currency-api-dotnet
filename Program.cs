using currency_api.data;
using currency_api.services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddHttpClient<CurrencyService>();
builder.Services.AddScoped<CurrencyService>();

// Adiciona suporte a Controllers 
builder.Services.AddControllers();

// Configura o Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construção da Aplicação
var app = builder.Build();

// Criação Automática do Banco de Dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); 
}

// Configuração do Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); 

// Inicia a API
app.Run(); 