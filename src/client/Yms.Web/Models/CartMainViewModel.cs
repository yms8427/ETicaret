using System.Collections.Generic;

namespace Yms.Web.Models
{
    public class CartMainViewModel
    {
        public CartMainViewModel()
        {
            ProductsOfCart = new List<CartViewModel>();
        }
        public List<CartViewModel> ProductsOfCart { get; set; }
        public decimal Total { get; set; }
    }
}