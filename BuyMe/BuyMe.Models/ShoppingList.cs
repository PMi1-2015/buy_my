﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
