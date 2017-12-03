using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public DateTime LastEditTime { get; set; }
        public DateTime ReminderTime { get; set; }
        [DefaultValue("Images/default.jpg")]
        public string ImagePath { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        [NotMapped]
        public double TotalPrice => Products.Sum(product => product.Price * product.Amount);
    }
}
