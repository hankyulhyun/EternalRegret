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
    }

    public class Current : ICurrent
    {
        private StockMeta _selectStock;

        public StockMeta GetSelectStock() => _selectStock;
        public void SetSelectStock(StockMeta selectStock) => _selectStock = selectStock;
    }
}
