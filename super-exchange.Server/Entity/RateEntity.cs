using System.ComponentModel.DataAnnotations.Schema;

namespace super_exchange.Server.Entity;

public class RateEntity
{
    public DateTime TradingDate { get; set; } = DateTime.MinValue;
    public DateTime EffectiveDate { get; set; } = DateTime.MinValue;
    public string Currency { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int Symbol { get; set; } = 0;
    public string Code { get; set; } = string.Empty;
    [Column(TypeName = "money")]
    public decimal Bid { get; set; } = decimal.Zero;
    [Column(TypeName = "money")]
    public decimal Ask { get; set; } = decimal.Zero;
    [Column(TypeName = "money")]
    public decimal Mid { get; set; } = decimal.Zero;
}
