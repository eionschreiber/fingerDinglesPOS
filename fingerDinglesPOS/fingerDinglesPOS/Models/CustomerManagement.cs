using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fingerDinglesPOS.Models
{
    public class CustomerManagement
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Allergies { get; set; }
        public string SpeicalNotes { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Product> AssociatedProducts { get; set; }
        public ICollection<Service> AssociatedServices { get; set; }
    }
}