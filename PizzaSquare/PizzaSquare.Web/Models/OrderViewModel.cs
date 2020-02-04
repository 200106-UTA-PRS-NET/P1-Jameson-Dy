using PizzaSquare.Lib;
using System.Collections.Generic;

namespace PizzaSquare.Web.Models
{
    public class OrderViewModel
    {
        public Dictionary<Pizzas, decimal> Pizzas { get; set; }
        public decimal Subtotal { get; set; }
        public int StoreID { get; set; }


    }
}
