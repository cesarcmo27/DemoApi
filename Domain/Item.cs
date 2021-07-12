using System;
using System.Collections.Generic;

namespace Domain
{
    public class Item
    {
        public Guid Id { get; set; }
        public string UPC { get; set; }
        public string Description { get; set; }
        public int MinimumOrderQuantity { get; set; }

        public string PurchaseUnitMeasure { get; set; }
        public decimal Cost { get; set; }

        public Guid ItemVendorId { get; set; }

        public ItemVendor ItemVendor { get; set; }

        public virtual ICollection<PharmacyInventory> Pharmacies { get; set; } 


    }
}