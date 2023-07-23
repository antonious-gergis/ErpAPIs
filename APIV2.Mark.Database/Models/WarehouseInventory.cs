using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Database.Models
{
    public class WarehouseInventory
    {
        [Key]
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public double? Quantity { get; set; }
        public long? WarehouseId { get; set; }
    }
}
