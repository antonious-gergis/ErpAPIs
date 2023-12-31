﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIV2.Mark.Entities.Dtos
{
    public class TaxDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = null!;
        public string NameAr { get; set; } = null!;
        public string? Type { get; set; }
        public string? Code { get; set; }
        public float Percentage { get; set; }
        public int StatusId { get; set; }
        public int SelectAccountTaxId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
