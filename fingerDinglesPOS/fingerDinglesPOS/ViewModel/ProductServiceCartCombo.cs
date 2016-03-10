using fingerDinglesPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fingerDinglesPOS.ViewModel
{
    public class ProductServiceCartCombo
    {
        public List<Product> productStuff { get; set; }
        public List<Service> serviceStuff { get; set; }

        public Cart cartStuff { get; set; }
    }
}