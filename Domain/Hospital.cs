using System;
using System.Collections.Generic;

namespace Domain
{
    public class Hospital
    {
        public Guid Id  { get; set; }
        public string Name  { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Pharmacy> Pharmacies { get; set; } 
    }
}