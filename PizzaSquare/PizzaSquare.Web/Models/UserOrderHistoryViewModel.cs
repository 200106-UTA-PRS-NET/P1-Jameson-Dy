using PizzaSquare.Lib;
using System;
using System.Collections.Generic;

namespace PizzaSquare.Web.Models
{
    public class UserOrderHistoryViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Subtotal { get; set; }
        public string StoreName { get; set; }

        public List<Pizzas> Pizzas { get; set; } 
    }
}
