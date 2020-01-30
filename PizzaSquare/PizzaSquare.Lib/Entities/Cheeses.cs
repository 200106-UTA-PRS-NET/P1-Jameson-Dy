﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaSquare.Lib
{
    public partial class Cheeses
    {
        public Cheeses()
        {
            Pizzas = new HashSet<Pizzas>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Pizzas> Pizzas { get; set; }
    }
}
