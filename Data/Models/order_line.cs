﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class order_line
    {
        public int order_id { get; set; }
        public int order_line_id { get; set; }
        public int? toy_code { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal sub_total { get; set; }
        public bool? wrapped { get; set; }

        public virtual orders order { get; set; }
        public virtual toys toy_codeNavigation { get; set; }
    }
}