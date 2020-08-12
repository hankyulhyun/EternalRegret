using EternalRegret.Common.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace EternalRegret.Cosmos.Model
{
    public class StockCosmos : Stock
    {
        [Key]
        public Guid Id { get; set; }
    }

}
