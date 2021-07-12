using System;
using System.Collections.Generic;

namespace Domain
{
    public class ItemVendor
    {
        public Guid Id { get; set; }
        public string Name  { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Item> Items { get; set; } 

    }
}