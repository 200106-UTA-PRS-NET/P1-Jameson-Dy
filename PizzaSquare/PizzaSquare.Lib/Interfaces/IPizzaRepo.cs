using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSquare.Lib.Interfaces
{
    public interface IPizzaRepo
    {
        public decimal GetPriceByID(int id);
    }
}
