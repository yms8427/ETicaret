using System.Collections.Generic;
using System.Linq;

namespace Yms.Web.Models
{
    public class CartMainViewModel
    {
        public List<CartViewModel> ProductsOfCart { get; set; }
        public decimal Total => ProductsOfCart.Sum(s => s.SubTotal);
    }
}