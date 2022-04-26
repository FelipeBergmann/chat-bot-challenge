using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Bot.ValueObjects
{
    public class StockPrice
    {
        public string Symbol { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public long Volume { get; set; }
    }
}
