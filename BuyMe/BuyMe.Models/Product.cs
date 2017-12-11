using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        [DefaultValue("Images/defaultProduct.jpg")]
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public virtual Category Category { get; set; }
    }
}
