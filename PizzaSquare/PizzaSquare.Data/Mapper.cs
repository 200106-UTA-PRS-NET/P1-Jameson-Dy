using PizzaSquare.Data.Models;
using PizzaSquare.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSquare.Data
{
    public static class Mapper
    {
        public static PizzasModel Map(Pizzas p)
        {
            return new PizzasModel()
            {
                Id = p.Id,
                Name = p.Name,
                Cheese = p.Cheese,
                Crust = p.Crust,
                Sauce = p.Sauce,
                Size = p.Size,
                Topping1 = p.Topping1,
                Topping2 = p.Topping2
            };
        }

    }
}
