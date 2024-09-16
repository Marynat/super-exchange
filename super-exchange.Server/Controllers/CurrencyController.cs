using Microsoft.AspNetCore.Mvc;
using super_exchange.Server.Dto;
using super_exchange.Server.Facade;

namespace super_exchange.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {

        private readonly ILogger<CurrencyController> _logger;
        private readonly ICurrencyFacade _facade;

        public CurrencyController(ILogger<CurrencyController> logger, ICurrencyFacade facade)
        {
            _logger = logger;
            _facade = facade;
        }

        [HttpGet(Name = "GetTodayCurrency")]
        public async Task<IEnumerable<CurrencyDto>> GetAsync()
        {

            return await _facade.GetLastCurrencies();
        }
    }
}
