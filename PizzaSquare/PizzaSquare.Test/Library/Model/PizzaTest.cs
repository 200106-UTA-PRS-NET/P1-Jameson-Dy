using PizzaSquare.Lib;
using Xunit;

namespace PizzaSquare.Test
{
    public class PizzaTest
    {
        readonly Pizzas pizzas = new Pizzas();

        [Fact]
        public void PizzaName_NonEmptyValue_StoresCorrectly()
        {
            const string randomPizzaNameValue = "Vegetarian";
            pizzas.Name = randomPizzaNameValue;
            Assert.Equal(randomPizzaNameValue, pizzas.Name);
        }

        [Fact]
        public void Cheese_EmptyPizzas_Null()
        {
            Assert.Null(pizzas.Cheese);
        }

        [Fact]
        public void Crust_EmptyPizzas_Null()
        {
            Assert.Null(pizzas.Crust);
        }

        [Fact]
        public void Sauce_EmptyPizzas_Null()
        {
            Assert.Null(pizzas.Sauce);
        }

        [Fact]
        public void Size_EmptyPizzas_Null()
        {
            Assert.Null(pizzas.Size);
        }
    }
}
