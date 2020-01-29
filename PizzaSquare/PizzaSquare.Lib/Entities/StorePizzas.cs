using System;
using System.Collections.Generic;

namespace PizzaSquare.Lib
{
    public partial class StorePizzas
    {
        public int StoreId { get; set; }
        public int PizzaId { get; set; }

        public virtual Pizzas Pizza { get; set; }
        public virtual Stores Store { get; set; }
    }
}
