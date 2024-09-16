using super_exchange.Server.Constants;
using super_exchange.Server.Document;
using super_exchange.Server.Exceptions;

namespace super_exchange.Server.Client;

public class NbpTableClient(HttpClient httpClient) : ITableClient
{
    private string BaseUrl = "https://api.nbp.pl/api/exchangerates/tables/";
    public async Task<TableDocument> GetTableExchangeRates(string table)
    {
        var uri = new Uri($"{BaseUrl}{table}");
        var response = await httpClient.GetFromJsonAsync<List<TableDocument>>(uri);

        if (response == null || response.Count == 0)
            throw new DocumentNotFoundException("Dod not find any table information");

        return response.FirstOrDefault();
    }
}

public interface ITableClient
{
    public Task<TableDocument> GetTableExchangeRates(string table);
}