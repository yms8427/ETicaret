using System;

namespace Yms.Web.Models
{
    public class AddToCartModel
    {
        public Guid ProductId { get; set; }
        public byte Count { get; set; }
    }
}