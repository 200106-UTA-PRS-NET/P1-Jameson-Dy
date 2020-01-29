using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSquare.Lib.Interfaces
{
    public interface IPizzaRepo
    {
        public decimal GetPriceByID(int id);
        public List<Cheeses> GetCheeseTypes();
        public List<Sauces> GetSauceTypes();
        public List<Crusts> GetCrustTypes();
        public List<Sizes> GetSizeTypes();
        public List<Toppings> GetToppingTypes();

    }
}
