namespace StockMarket.Bot.Configurations
{
    public class StockIntegrationConfiguration
    {
        public string StockUrl { get; set; } = "https://stooq.com";
        public string GetStockRoute { get; set; } = "/q/l/";
    }
}
