
namespace currency_api.models;

public class ConversionLog
{

    public int Id { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.Now;

    public string FromCurrency { get; set; } = string.Empty; 
    public string ToCurrency { get; set; } = string.Empty;   
    
    public decimal OriginalAmount { get; set; }  
    public decimal ConvertedAmount { get; set; } 

    public decimal ExchangeRate { get; set; }    
}