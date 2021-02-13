using System;

namespace Yms.Web.Models
{
    public class UpdateCartViewModel
    {
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}