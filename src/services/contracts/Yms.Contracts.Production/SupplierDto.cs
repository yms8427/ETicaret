using System;

namespace Yms.Contracts.Production
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public int Vote { get; set; }
        public int VoteCount { get; set; }
    }
}
