﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Database.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public long AccountId { get; set; }
        public int PurchaseId { get; set; }
        public string Code { get; set; } = null!;
        public float Amount { get; set; }
        public int VendorId { get; set; }
        public int EmpId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
    }
}
