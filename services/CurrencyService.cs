
using System.Text.Json;
using currency_api.data;
using currency_api.models;

namespace currency_api.services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;

    // Injeção de Dependência
    public CurrencyService(HttpClient httpClient, AppDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<ConversionLog> ConvertCurrency(string from, string to, decimal amount)
    {
        // Monta a URL da API (ex: https://economia.awesomeapi.com.br/last/USD-BRL)
        var pair = $"{from}-{to}";
        var url = $"https://economia.awesomeapi.com.br/last/{pair}";

        // Faz a requisição HTTP
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Erro ao consultar a API externa.");
        }

        // Lê o JSON de resposta
        var json = await response.Content.ReadAsStringAsync();
        
        // A API retorna uma chave dinâmica (ex: "USDBRL"), precisamos encontrar ela
        using var doc = JsonDocument.Parse(json);
        var key = pair.Replace("-", ""); // Remove o hífen para bater com a chave do JSON

        if (!doc.RootElement.TryGetProperty(key, out var element))
        {
            throw new Exception("Par de moedas não encontrado.");
        }

        // Pega o valor de compra ("bid") e converte para decimal
        var bidText = element.GetProperty("bid").GetString();
        var rate = decimal.Parse(bidText, System.Globalization.CultureInfo.InvariantCulture);

        // Cria o objeto de log e Salva no Banco
        var log = new ConversionLog
        {
            FromCurrency = from.ToUpper(),
            ToCurrency = to.ToUpper(),
            OriginalAmount = amount,
            ExchangeRate = rate,
            ConvertedAmount = amount * rate,
            Timestamp = DateTime.Now
        };

        _context.Conversions.Add(log);
        await _context.SaveChangesAsync();

        return log;
    }

    // Retorna a lista de logs do banco
    public List<ConversionLog> GetHistory()
    {
        // Acessa a tabela Conversions
        // Ordena por Data (do mais novo para o mais antigo)
        // Pega apenas 10
        // Transforma em Lista
        return _context.Conversions
                       .OrderByDescending(c => c.Timestamp)
                       .Take(10)
                       .ToList();
    }
}