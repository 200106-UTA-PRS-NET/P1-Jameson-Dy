﻿using System.Collections.Generic;

namespace PizzaSquare.Lib.Interfaces
{
    public interface IOrderRepo
    {
        public void AddPizzaToOrder(Pizzas p, decimal price);
        public List<Pizzas> GetOrderedPizzas();
        public void SetCurrentPizza(Pizzas p);
        public Pizzas GetCurrentPizza();
        public bool SubmitOrder(int userID, int storeID);
        public List<Orders> GetStoreOrderHistoryById(int storeID);
        public List<Pizzas> GetOrderedPizzasByOrderId(int orderID);
        public List<Orders> GetUserOrderHistoryById(int userID);
        public void ClearOrder();
    }
}
