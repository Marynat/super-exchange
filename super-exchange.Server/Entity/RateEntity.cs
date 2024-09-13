namespace super_exchange.Server.Entity;

public class RateEntity
{
    public DateTime TradingDate { get; set; } = DateTime.Now;
    public DateTime EffectiveDate { get; set; } = DateTime.Now;
    public string Currency { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int Symbol { get; set; } = 0;
    public string Code { get; set; } = string.Empty;
    public decimal Bid { get; set; } = decimal.Zero;
    public decimal Ask { get; set; } = decimal.Zero;
    public decimal Mid { get; set; } = decimal.Zero;
}
