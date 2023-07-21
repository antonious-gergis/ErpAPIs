using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class WarehouseInventory
    {
        [Key]
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public double? Quantity { get; set; }
        public int? WarehouseId { get; set; }
    }
}
