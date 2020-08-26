using EternalRegret.Common.Model;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Json;

namespace EternalRegret.State
{
    public interface ICurrent
    {
        StockMeta GetSelectStock();
        void SetSelectStock(StockMeta selectStock);

        void SetStockPrices(Stock stock);
    }

    public class Current : ICurrent
    {
        private StockMeta _selectStock;
        private Stock _stock;

        public StockMeta GetSelectStock() => _selectStock;
        public void SetSelectStock(StockMeta selectStock) => _selectStock = selectStock;

        public void SetStockPrices(Stock stock) => _stock = stock;
    }
}
