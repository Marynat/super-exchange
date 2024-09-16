using AutoMapper;
using super_exchange.Server.Constants;
using super_exchange.Server.Document;
using super_exchange.Server.Entity;

namespace super_exchange.Server.Mapper
{
    public class RateMapper(IMapper _mapper) : IRateMapper
    {
        public List<RateEntity> Map(TableDocument tableA, TableDocument tableC)
        {
            List<RateEntity> rates = new();
            var joinedTables = tableA.Rates.Join(tableC.Rates, a => a.Code, c => c.Code, (a, c) => new RateEntity()
            {
                Code = a.Code,
                Country = a.Country,
                Currency = a.Currency,
                Ask = c.Ask,
                Bid = c.Bid,
                Mid = a.Mid,
                Symbol = a.Symbol,
                TradingDate = tableA.TradingDate,
                EffectiveDate = tableA.EffectiveDate

            }).ToList();

            return joinedTables;
        }

        public List<RateEntity> Map(TableDocument table)
        {
            List<RateEntity> rates = new();
            foreach (var rate in table.Rates)
            {
                var entity = _mapper.Map<RateEntity>(rate);
                entity.TradingDate = table.TradingDate;
                entity.EffectiveDate = table.EffectiveDate;
                rates.Add(entity);
            }
            return rates;
        }
    }
}
