using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EternalRegret.Cosmos.Model
{
    public class Stock
    {
        [Key]
        public Guid Id { get; set; }

        public string StockName { get; set; }

        public string StockCode { get; set; }

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
