using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderTracking.Entities
{
    public class Product : BaseEntity                                                         
    {      
        [StringLength(20)]
        public string Barcode { get; set; }     
        
        [StringLength(300)]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
