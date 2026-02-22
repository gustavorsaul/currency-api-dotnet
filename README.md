## Currency API

API simples para conversao de moedas usando a [AwesomeAPI](https://docs.awesomeapi.com.br/api-de-moedas) e registro de historico em SQLite.

### Estrutura do log
- `id`
- `timestamp`
- `fromCurrency`
- `toCurrency`
- `originalAmount`
- `convertedAmount`
- `exchangeRate`

### Requisitos
- .NET SDK 8

### Como rodar
1. Restaurar e executar
	 - `dotnet restore`
	 - `dotnet run`

### Endpoints
- `POST /api/currency/convert`
	- Body JSON:
		```json
		{
            "id": 1,
            "timestamp": "2026-01-22T18:46:12.8317233-03:00",
            "fromCurrency": "USD",
            "toCurrency": "BRL",
            "originalAmount": 300,
            "convertedAmount": 1552.9500,
            "exchangeRate": 5.1765
        }
		```
- `GET /api/currency/history`
    - Body JSON:
        ```json
		[
            {
                "id": 2,
                "timestamp": "2026-01-01T00:00:00Z",
                "fromCurrency": "USD",
                "toCurrency": "BRL",
                "originalAmount": 300.0,
                "convertedAmount": 1552.95,
                "exchangeRate": 5.1765
            },
            {
                "id": 1,
                "timestamp": "2026-01-01T00:00:00Z",
                "fromCurrency": "USD",
                "toCurrency": "BRL",
                "originalAmount": 100.0,
                "convertedAmount": 519.48,
                "exchangeRate": 5.1948
            }
        ]

        
		```

### Observações
- O historico retorna os 10 ultimos registros.
