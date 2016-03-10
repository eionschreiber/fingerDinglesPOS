using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace fingerDinglesPOS.Models
{
    public class FingerDinglesPosDB : DbContext
    {
        public FingerDinglesPosDB(): base("DefaultConnection")
        {

        }
        public DbSet<Product> product { get; set; }
        public DbSet<Service> service { get; set; }
        public DbSet<CustomerManagement> customerManagement { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
    }
}