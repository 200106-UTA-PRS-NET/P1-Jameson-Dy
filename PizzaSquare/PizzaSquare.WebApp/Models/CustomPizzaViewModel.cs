using PizzaSquare.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaSquare.Web.Models
{
    public class CustomPizzaViewModel
    {
        public List<Crusts> Crusts { get; set; }
        public List<Sauces> Sauces { get; set; }
        public List<Cheeses> Cheeses { get; set; }
        public List<Sizes> Sizes { get; set; }

        public int CheeseId { get; set; }
        public int CrustId { get; set; }
    }
}
