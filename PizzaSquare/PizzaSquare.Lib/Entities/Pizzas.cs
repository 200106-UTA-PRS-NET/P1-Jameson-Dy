using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaSquare.Lib
{
    public partial class Pizzas
    {
        public Pizzas()
        {
            OrderPizzas = new HashSet<OrderPizzas>();
            StorePizzas = new HashSet<StorePizzas>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CrustId { get; set; }
        public int? CheeseId { get; set; }
        public int? SauceId { get; set; }
        public int? SizeId { get; set; }
        public int? Topping1Id { get; set; }
        public int? Topping2Id { get; set; }

        public virtual Cheeses Cheese { get; set; }
        public virtual Crusts Crust { get; set; }
        public virtual Sauces Sauce { get; set; }
        public virtual Sizes Size { get; set; }
        public virtual Toppings Topping1 { get; set; }
        public virtual Toppings Topping2 { get; set; }
        public virtual ICollection<OrderPizzas> OrderPizzas { get; set; }
        public virtual ICollection<StorePizzas> StorePizzas { get; set; }
    }
}
