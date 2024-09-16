namespace super_exchange.Server.Dto;

public class CurrencyDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal Ask { get; set; } = decimal.Zero;
    public decimal Bid { get; set; } = decimal.Zero;
    public decimal Mid { get; set; } = decimal.Zero;
}
