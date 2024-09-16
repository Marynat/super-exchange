using super_exchange.Server.Document;
using super_exchange.Server.Entity;

namespace super_exchange.Server.Mapper
{
    public interface IRateMapper
    {
        public List<RateEntity> Map(TableDocument tableA, TableDocument tableC);
        public List<RateEntity> Map(TableDocument table);
    }
}