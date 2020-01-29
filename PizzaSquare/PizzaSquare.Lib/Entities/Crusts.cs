using System;
using System.Collections.Generic;

namespace PizzaSquare.Lib
{
    public partial class Crusts
    {
        public Crusts()
        {
            Pizzas = new HashSet<Pizzas>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Pizzas> Pizzas { get; set; }
    }
}
