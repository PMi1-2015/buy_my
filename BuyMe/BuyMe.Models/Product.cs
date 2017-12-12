using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.Models
{
    public class Product
    {
        [Key]
        public string Name { get; set; }
        public double Price { get; set; }
        [DefaultValue("Images/defaultProduct.jpg")]
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public virtual Category Category { get; set; }
    }
}
