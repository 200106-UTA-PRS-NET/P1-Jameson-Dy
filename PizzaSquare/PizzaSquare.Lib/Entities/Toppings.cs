using System;
using System.Collections.Generic;

namespace PizzaSquare.Lib
{
    public partial class Toppings
    {
        public Toppings()
        {
            PizzasTopping1 = new HashSet<Pizzas>();
            PizzasTopping2 = new HashSet<Pizzas>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pizzas> PizzasTopping1 { get; set; }
        public virtual ICollection<Pizzas> PizzasTopping2 { get; set; }
    }
}
