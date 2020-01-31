using PizzaSquare.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaSquare.Web.Models
{
    public class OrderViewModel
    {
        public Dictionary<Pizzas, decimal> Pizzas { get; set; }
        public decimal Subtotal { get; set; }
        public int StoreID { get; set; }


    }
}
