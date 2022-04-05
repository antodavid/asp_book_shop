using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Book_Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }

        public double amount { get; set; }

        public string CardNumber { get; set; }
        public string CardExpDate { get; set; }
        public string CardCvv { get; set; }
        public string ShippingAddress { get; set; }
        public IList<Book> Books { get; set; }      
              
    }
}