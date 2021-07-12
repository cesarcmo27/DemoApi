using System;
using System.Collections.Generic;

namespace Domain
{
    public class Pharmacy
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Guid HospitalId { get; set; }

        public Hospital Hospital { get; set; }

        public virtual ICollection<PharmacyInventory> PharmaciesInventory { get; set; } 


    }
}