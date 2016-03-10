using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace fingerDinglesPOS.Models
{
    public class Product
    {
        public int ID { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public string ProductImageName { get; set; }
    }
}