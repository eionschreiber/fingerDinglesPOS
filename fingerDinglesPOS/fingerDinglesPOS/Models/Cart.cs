using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fingerDinglesPOS.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public virtual ICollection<Product> ProductID { get; set; }
        public virtual ICollection<Service> serviceList { get; set; }
        public DateTime checkoutTime { get; set; }
        public decimal TransactionTotal { get; set; }
    }
}