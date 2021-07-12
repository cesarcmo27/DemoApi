using System;
using System.Collections.Generic;

namespace Domain
{
    public class Order{
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int ClientId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } 
    }
    
}