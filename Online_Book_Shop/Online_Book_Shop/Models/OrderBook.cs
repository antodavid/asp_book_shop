using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Book_Shop.Models
{
    public class OrderBook
    {
        public int Id { get; set; }     
        public Book Book { get; set; }
        public int BookId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}