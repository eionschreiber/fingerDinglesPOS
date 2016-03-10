using fingerDinglesPOS.Models;
using fingerDinglesPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace fingerDinglesPOS.Controllers
{
    public class CashRegisterController : Controller
    {
        private FingerDinglesPosDB db = new FingerDinglesPosDB();
        //public List<object> productServiceList = new List<object>();
        static List<Product> productList = new List<Product>();
        static List<int> productIDList = new List<int>();
        static List<Service> serviceList = new List<Service>() { };
        static List<decimal> transactionPreview = new List<decimal>() { };
        public ProductServiceCartCombo previewTransactionModel = new ProductServiceCartCombo();
        // GET: CashRegister
        public ActionResult Index()
        {
            ProductServiceCombo productServiceComboView = new ProductServiceCombo();

           // previewTransactionModel.productStuff = db.product.ToList();
            productServiceComboView.productStuff = (from n in db.product where n.Quantity != 0 select n).ToList();
            productServiceComboView.serviceStuff = (from n in db.service where n.ServiceImageName != null select n).ToList();

            return View(productServiceComboView);
        }

        [HttpPost]
        public ActionResult AddToProductList(string itemName)
        {
            decimal salesPrice = 0;
            string productName = itemName;
            int id = 0;
            var data = (from c in db.product where c.ProductName == itemName && c.Quantity !=0 select c );
            foreach (var stuff in data)
            {
                productName = stuff.ProductName;
                salesPrice = stuff.SalesPrice;
                id = stuff.ID;
            }

            Product item = new Product()
            {
                ProductName = productName,
                SalesPrice = salesPrice,
                ID = id
            };
            productList.Add(item);
            transactionPreview.Add(item.SalesPrice);
            return RedirectToAction("Index", "CashRegister");
        }
        public ActionResult AddToServiceList(string itemName)
        {
            string productName = itemName;
            decimal salesPrice = 0;
            int id = 0;
            var data = (from c in db.service where c.ServiceName == itemName && c.ServiceImageName != null select c);
            foreach (var stuff in data)
            {
                productName = stuff.ServiceName;
                salesPrice = stuff.SalePrice;
                id = stuff.ID;
            }
            productIDList.Add(id);
            //var price = Convert.ToInt32(from c in db.service where c.ServiceName == itemName select c.SalePrice);
            Service item = new Service()
            {
                ServiceName = productName,
                SalePrice = salesPrice,
                ID = id
            };
            serviceList.Add(item);
            transactionPreview.Add(item.SalePrice);
            return RedirectToAction("Index", "CashRegister");
        }
        public ActionResult PreviewTransaction()
        {
            decimal allItemsTotalled = transactionPreview.Sum();

            Cart cart = new Cart()
            {
                checkoutTime = DateTime.Now,
                TransactionTotal = allItemsTotalled,
                serviceList = serviceList,
                ProductID = productList

            };
            db.Cart.Add(cart);
            db.SaveChanges();
            return View(cart);
        }
        [HttpPost]
        public ActionResult AddTransaction(string otherEmail)
        {
            
            //foreach (var item in productList)
            //{
            //    var newQuantity = (from x in db.product where x.ID == item.ID select x.Quantity);
            //}

            productList.Clear();
            serviceList.Clear();
            transactionPreview.Clear(); 
            Cart newestTransaction = (from l in db.Cart orderby l.ID descending select l).First();
            

            if (otherEmail != "")
            {
            List<string> listOfMessage = new List<string>();
            listOfMessage.Add("Transactions Total: $"+newestTransaction.TransactionTotal.ToString());
            listOfMessage.Add("Time: " + newestTransaction.checkoutTime);   
            foreach (var item in newestTransaction.ProductID)
            {
                listOfMessage.Add(item.ProductName);
                listOfMessage.Add(item.SalesPrice.ToString());
            }
            foreach (var item in newestTransaction.serviceList)
            {
                listOfMessage.Add(item.ServiceName);
                listOfMessage.Add(item.SalePrice.ToString());
            }
            string eBody = string.Join(",",listOfMessage.ToArray());
            string recipetTemplate = "Thank you for your purchase at Finger Dingles Nail Salon your recipt: ";
            string sendingMessage = recipetTemplate + eBody;
            MailMessage message = new MailMessage("eionschreiber@gmail.com", otherEmail);
            message.Subject = "Recipt from Finger Dingles Nail Salon";
            message.Body = eBody;
            SmtpClient mailer = new SmtpClient("smtp.gmail.com", 587);
            mailer.Credentials = new NetworkCredential("eionschreiber@gmail.com", "thelegendofzeldaN64");
            mailer.EnableSsl = true;
            mailer.Send(message);
            }
            

            return View(newestTransaction);
        }
    }
}