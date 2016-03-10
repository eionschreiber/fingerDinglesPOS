using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace fingerDinglesPOS.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal SalePrice { get; set; }
        public string ServiceImageName { get; set; }

    }
}