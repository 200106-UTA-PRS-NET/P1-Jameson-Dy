using System.Collections.Generic;

namespace PizzaSquare.Lib.Interfaces
{
    public interface IPizzaRepo
    {
        public Pizzas GetPizzaById(int id);
        public decimal GetPriceByID(int id);
        public List<Cheeses> GetCheeseTypes();
        public List<Sauces> GetSauceTypes();
        public List<Crusts> GetCrustTypes();
        public List<Sizes> GetSizeTypes();
        public List<Toppings> GetToppingTypes();
        public Pizzas MapPizzaByIDs(int crustID, int sauceID, int cheeseID, int sizeID, int topping1ID, int topping2ID);
        public decimal GetPriceByPizza(Pizzas p);
    }
}
