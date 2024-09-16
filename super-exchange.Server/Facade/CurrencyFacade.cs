using AutoMapper;
using Microsoft.EntityFrameworkCore;
using super_exchange.Server.Database;
using super_exchange.Server.Dto;
using super_exchange.Server.Entity;

namespace super_exchange.Server.Facade;

public class CurrencyFacade(ExchangeDatabaseContext _context, IMapper _mapper, ILogger<CurrencyFacade> _logger) : ICurrencyFacade
{
    public async Task<List<CurrencyDto>> GetLastCurrencies()
    {
        _logger.LogInformation("Getting currencies from database");
        var lastDate = await _context.RateEntities.MaxAsync(c => c.EffectiveDate);
        var entities = await _context.RateEntities.Where(r => r.EffectiveDate.Equals(lastDate)).ToListAsync();
        return _mapper.Map<List<CurrencyDto>>(entities);
    }
}

public interface ICurrencyFacade
{
    Task<List<CurrencyDto>> GetLastCurrencies();
}