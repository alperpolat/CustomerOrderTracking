using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrderTracking.Entities
{
    public class Customer : BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }                         
        
        [StringLength(350)]
        public string Address { get; set; }

        public List<Order> Orders { get; set; }
    }
}
