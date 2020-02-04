using PizzaSquare.Lib;
using System;
using System.Collections.Generic;

namespace PizzaSquare.Web.Models
{
    public class StoreOrderHistoryViewModel
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public decimal Subtotal { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Pizzas> OrderedPizzas { get; set; }

    }
}
