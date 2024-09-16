using Moq;
using Moq.Protected;
using super_exchange.Server.Client;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Text.Json;
using super_exchange.Server.Document;
using super_exchange.Server.Constants;
using super_exchange.Server.Exceptions;

namespace super_exchange.ServerTest.Client;

public class NbpClientTest
{

    [Fact]
    public async Task GetTableExchangeRates_ShouldReturnData()
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(PrepareTableDocument(1)),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            });

        var client = new NbpTableClient(new HttpClient(mockMessageHandler.Object));
        var docs = await client.GetTableExchangeRates(NbpTable.TableA);
        Assert.NotNull(docs);

    }
    [Fact]
    public async Task GetTableExchangeRates_ShouldThrowErrorWhenNoDataWasFound()
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(PrepareTableDocument(0)),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            });

        var client = new NbpTableClient(new HttpClient(mockMessageHandler.Object));
        await Assert.ThrowsAsync<DocumentNotFoundException>(async () => await client.GetTableExchangeRates(NbpTable.TableA));

    }

    private List<TableDocument> PrepareTableDocument(int count)
    {
        List<TableDocument> documents = new();
        for (int i = 0; i < count; i++)
        {
            documents.Add(new()
            {
                EffectiveDate = DateTime.Now,
                No = i.ToString(),
                Table = $"Table{i}",
                TradingDate = DateTime.MinValue,
                Rates = new List<RateDocument>() { new RateDocument(){
                    Ask = 1,
                    Bid = 1,
                    Mid = 1,
                    Code = "USD",
                    Country ="USA",
                    Currency = "Dolar"
                }}
            });
        }
        return documents;
    }

}
