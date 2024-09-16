using super_exchange.Server.Document;
using super_exchange.Server.Entity;
using AutoMapper;
using super_exchange.Server.Dto;

namespace super_exchange.Server.Mapper
{
    public class RateProfile : Profile
    {
        public RateProfile()
        {
            CreateMap<RateDocument, RateEntity>()
                .ForMember(e => e.Ask, x => x.MapFrom(d => d.Ask))
                .ForMember(e => e.Bid, x => x.MapFrom(d => d.Bid))
                .ForMember(e => e.Mid, x => x.MapFrom(d => d.Mid))
                .ForMember(e => e.Currency, x => x.MapFrom(d => d.Currency))
                .ForMember(e => e.Symbol, x => x.MapFrom(d => d.Symbol))
                .ForMember(e => e.Country, x => x.MapFrom(d => d.Country))
                .ForMember(e => e.Code, x => x.MapFrom(d => d.Code));

            CreateMap<RateEntity, CurrencyDto>()
                .ForMember(c => c.Name, x => x.MapFrom(e => e.Currency));
        }
    }
}
