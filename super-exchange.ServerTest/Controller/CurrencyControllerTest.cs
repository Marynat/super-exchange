using Microsoft.Extensions.Logging;
using Moq;
using super_exchange.Server.Controllers;
using super_exchange.Server.Facade;

namespace super_exchange.ServerTest.Controller;

public class CurrencyControllerTest
{
    private readonly CurrencyController _controller;
    private readonly Mock<ICurrencyFacade> _facade = new Mock<ICurrencyFacade>();
    private readonly Mock<ILogger<CurrencyController>> _logger = new Mock<ILogger<CurrencyController>>();

    public CurrencyControllerTest()
    {
        _controller = new(_logger.Object, _facade.Object);


    }

    [Fact]
    public async Task ControllerShouldCallFacaseToFetchData()
    {
        await _controller.GetAsync();

        _facade.Verify(f => f.GetLastCurrencies(), Times.Once());
    }
}
