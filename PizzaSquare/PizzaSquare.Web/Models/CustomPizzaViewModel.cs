using PizzaSquare.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaSquare.Web.Models
{
    public class CustomPizzaViewModel
    {
        public List<Crusts> crusts { get; set; }
        public List<Sauces> sauces { get; set; }
        public List<Cheeses> cheeses { get; set; }
        public List<Sizes> sizes { get; set; }

    }
}
