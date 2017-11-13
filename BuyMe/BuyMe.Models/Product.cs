﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.Models
{
    public class Product
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public virtual Category Category { get; set; }
    }
}
