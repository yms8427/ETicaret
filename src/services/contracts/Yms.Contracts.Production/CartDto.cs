using System;

namespace Yms.Contracts.Production
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public byte Amount { get; set; }
        public decimal Price { get; set; }
        public Guid? ImageId { get; set; }
        public decimal SubTotal
        {
            get
            {
                return Price * Amount;
            }
        }
    }
}