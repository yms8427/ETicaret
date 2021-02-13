using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    public class CartViewModel
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public byte Amount { get; set; }
        [Required]
        public decimal SubTotal { get; set; }
        [Required]
        public Guid? ImageId { get; set; }
    }
}