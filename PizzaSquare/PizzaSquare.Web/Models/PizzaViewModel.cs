using PizzaSquare.Lib;

namespace PizzaSquare.Web.Models
{
    public class PizzaViewModel
    {
        public string Name { get; set; }
        public Crusts Crust { get; set; }
        public Sauces Sauce { get; set; }
        public Cheeses Cheese { get; set; }
        public Sizes Size { get; set; }
        public Toppings Topping1 { get; set; }
        public Toppings Topping2 { get; set; }
        public decimal Price { get; set; }

    }
}
