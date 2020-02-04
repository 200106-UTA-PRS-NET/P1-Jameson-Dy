using PizzaSquare.Lib;
using System.Collections.Generic;

namespace PizzaSquare.Data.Models
{
    public class PizzaTypeModel
    {
        public List<Cheeses> Cheeses { get; set; }
        public List<Sauces> Sauces { get; set; }
        public List<Sizes> Sizes { get; set; }
        public List<Crusts> Crusts { get; set; }
        public List<Toppings> Toppings { get; set; }

    }
}
