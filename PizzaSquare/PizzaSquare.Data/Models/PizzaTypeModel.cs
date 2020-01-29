using PizzaSquare.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSquare.Data.Models
{
    public class PizzaTypeModel
    {
        public List<Cheeses> Cheeses { get; set; }
        public List<Sauces> Sauces { get; set; }
        public List<Sizes> Sizes { get; set; }
        public List<Crusts> Crusts { get; set; }

    }
}
