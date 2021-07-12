using System;

namespace Domain
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public int IdProducto { get; set; }
        public int Count { get; set; }

        public decimal Amount { get; set; }

        public Guid OrderId { get; set; }
         public Order Order { get; set; }
    }
}