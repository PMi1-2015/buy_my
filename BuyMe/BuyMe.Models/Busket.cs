using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.Models
{
    public class Busket
    {
        public int Id { get; set; }
        public virtual ICollection<Product> ChosenProducts { get; set; }
    }
}
