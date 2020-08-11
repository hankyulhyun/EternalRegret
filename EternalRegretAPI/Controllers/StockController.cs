using EternalRegret.Cosmos.Context;
using EternalRegret.Cosmos.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;

namespace EternalRegretAPI.Controllers
{
    [Route("v1/[controller]")]
    // [Produces("application/json")]
    public class StockController : ControllerBase
    {
        private readonly StockContext _stockContext;

        public StockController(StockContext stockService)
        {
            _stockContext = stockService;
        }

        [HttpGet("meta")]
        public IActionResult Get()
        {
            try
            {
                var list = _stockContext.Stocks
                    .Where(s => true)
                    .Select(s => new StockMeta
                    {
                        StockName = s.StockName,
                        StockCode = s.StockCode,
                    }).ToList();

                if (list == null)
                    return NotFound(null);

                return Ok(list);
            }
            catch (Exception ex)
            {
                // Some logging here?
                return null;
            }
        }

        [HttpGet("{code}")]
        public IActionResult Get(string code)
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
            if (stock == null)
                return NotFound(stock);

            return Ok(stock);
        }
    }
}