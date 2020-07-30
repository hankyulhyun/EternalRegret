using EternalRegret.MongoDB.Model;
using EternalRegret.MongoDB.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace EternalRegretAPI.Controllers
{
    [Route("v1/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("{code}/{date}")]
        public string Get(string code, string date)
        {

            Stock stock = null;
            try
            {
                var dateInfo = DateTime
                    .ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
                    .ToUniversalTime();

                stock = _stockService.Get(code);
                stock.Prices.RemoveAll(p => p.PriceDate < dateInfo);
            }
            catch (Exception ex)
            {
                // Some logging here?
                return null;
            }


            return JsonConvert.SerializeObject(stock);
        }

    }
}