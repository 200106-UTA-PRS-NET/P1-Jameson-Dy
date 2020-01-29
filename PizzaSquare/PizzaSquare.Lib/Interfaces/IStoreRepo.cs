using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSquare.Lib.Interfaces
{
    public interface IStoreRepo
    {
        public IEnumerable<Stores> GetStores();

    }
}
