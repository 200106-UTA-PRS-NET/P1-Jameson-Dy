using PizzaSquare.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSquare.Data.Models
{
    public class PizzasModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Cheeses Cheese { get; set; }
        public Crusts Crust { get; set; }
        public Sauces Sauce { get; set; }
        public Sizes Size { get; set; }


        public Toppings Topping1 { get; set; }
        public Toppings Topping2 { get; set; }
        public decimal Price { get; set; }
    }
}
