﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class toys
    {
        public toys()
        {
            order_line = new HashSet<order_line>();
            price_history = new HashSet<price_history>();
        }

        public int code { get; set; }
        public string name { get; set; }
        public int? category_id { get; set; }
        public int stock { get; set; }
        public int stock_threshold { get; set; }
        public bool? state { get; set; }

        public virtual categories category { get; set; }
        public virtual ICollection<order_line> order_line { get; set; }
        public virtual ICollection<price_history> price_history { get; set; }
    }
}