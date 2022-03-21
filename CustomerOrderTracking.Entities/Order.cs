using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderTracking.Entities
{
    public class Order : BaseEntity                                                            
    {
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [StringLength(350)]
        public string Address { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
    }
}
