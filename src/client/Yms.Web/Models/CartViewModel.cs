using System;
using System.ComponentModel.DataAnnotations;

namespace Yms.Web.Models
{
    public class CartViewModel
    {
        [Required]
        public Guid Id { get; set; }
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