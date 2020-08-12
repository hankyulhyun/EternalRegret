using System;
using System.Collections.Generic;

namespace EternalRegret.Common.Model
{
    public class StockMeta
    {
        public string StockName { get; set; }

        public string StockCode { get; set; }
    }

    public class Stock : StockMeta
    {
        public List<Price> Prices { get; set; }

        public Stock()
        {
            Prices = new List<Price>();
        }
    }

    public class Price
    {
        public DateTime PriceDate { get; set; }

        public int StartPrice { get; set; }

        public int EndPrice { get; set; }
        
        public int LowPrice { get; set; }
        
        public int HighPrice { get; set; }
        
        public int Volumn { get; set; }
    }
}
