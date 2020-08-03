using EternalRegret.Cosmos.Context;
using EternalRegret.Cosmos.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace EternalRegretAPI.Controllers
{
    [Route("v1/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockContext _stockContext;

        public StockController(StockContext stockService)
        {
            _stockContext = stockService;
        }

        [HttpGet("meta")]
        public string Get()
        {

            try
            {
                var list = _stockContext.Stocks
                    .Where(s => true)
                    .Select(s => new Stock
                    {
                        StockName = s.StockName,
                        StockCode = s.StockCode,
                    }).ToList();

                return (list == null) ? null : JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                // Some logging here?
                return null;
            }
        }

        [HttpGet("{code}")]
        public string Get(string code)
        {

            Stock stock = null;
            try
            {
                stock = _stockContext.Stocks
                    .Where(s => s.StockCode == code)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Some logging here?
                return null;
            }


            return (stock == null) ? null : JsonConvert.SerializeObject(stock);
        }
    }
}