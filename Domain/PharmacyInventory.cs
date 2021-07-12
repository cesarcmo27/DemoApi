using System;

namespace Domain
{
    public class PharmacyInventory
    {
        public int QuantityOnHand { get; set; }
        public decimal UnitPrice { get; set; }
        public int ReorderQuantity { get; set; }
        public string UnidOfMeasure { get; set; }

        public Guid PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public Guid ItemId { get; set; }
        public Item Item { get; set; }

    }
}