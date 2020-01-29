using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSquare.Lib.Interfaces
{
    public interface IStoreRepo
    {
        public IEnumerable<Stores> GetStores();

        public IEnumerable<Pizzas> GetStorePizzasById(int id);
        public Stores GetStoreById(int id);
    }
}
