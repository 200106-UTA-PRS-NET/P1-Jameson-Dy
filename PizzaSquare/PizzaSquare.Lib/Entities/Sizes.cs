using System;
using System.Collections.Generic;

namespace PizzaSquare.Lib
{
    public partial class Sizes
    {
        public Sizes()
        {
            Pizzas = new HashSet<Pizzas>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Pizzas> Pizzas { get; set; }
    }
}
