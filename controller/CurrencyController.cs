
using Microsoft.AspNetCore.Mvc;
using currency_api.services;

namespace currency_api.controller;

[ApiController]
[Route("api/[controller]")] // Define a rota base como: api/currency
public class CurrencyController : ControllerBase
{
    private readonly CurrencyService _service;

    // Injeção de Dependência: O Controller pede o Service para funcionar
    public CurrencyController(CurrencyService service)
    {
        _service = service;
    }

    // POST: api/currency/convert
    [HttpPost("convert")]
    public async Task<IActionResult> Convert([FromBody] ConversionRequest request)
    {
        try
        {
            // Chama o serviço que criamos antes
            var resultado = await _service.ConvertCurrency(request.From, request.To, request.Amount);
            
            // Retorna HTTP 200 (OK) 
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            // Retorna HTTP 400 (Erro) 
            return BadRequest(new { error = ex.Message });
        }
    }

    // GET: api/currency/history
    [HttpGet("history")]
    public IActionResult GetHistory()
    {
        // Chama o serviço e retorna a lista JSON
        var history = _service.GetHistory();
        return Ok(history);
    }
}

// DTO: simplificação para receber os dados do JSON
public record ConversionRequest(string From, string To, decimal Amount);

