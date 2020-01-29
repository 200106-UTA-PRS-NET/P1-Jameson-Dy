using System;
using System.Collections.Generic;

namespace PizzaSquare.Lib
{
    public partial class OrderPizzas
    {
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public int? Count { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Pizzas Pizza { get; set; }
    }
}
