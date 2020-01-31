using PizzaSquare.Lib;
using PizzaSquare.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaSquare.Data.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        PizzaSquareContext db;
        static Dictionary<Pizzas, decimal> orderedPizzas = new Dictionary<Pizzas, decimal>();
        static Pizzas currPizza = new Pizzas();

        public OrderRepo()
        {
            db = new PizzaSquareContext();
        }
        public OrderRepo(PizzaSquareContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddPizzaToOrder(Pizzas p, decimal price)
        {
            orderedPizzas.Add(p, price);
        }

        public List<Pizzas> GetOrderedPizzas()
        {
            return new List<Pizzas>(orderedPizzas.Keys);
        }

        public void SetCurrentPizza(Pizzas p)
        {
            currPizza = p;
        }

        public Pizzas GetCurrentPizza()
        {
            return currPizza;
        }



        public bool SubmitOrder(int userID, int storeID)
        {

            decimal subtotal = 0;
            foreach (decimal price in orderedPizzas.Values)
            {
                subtotal += price;
            }

            Orders order = new Orders()
            {
                UserId = userID,
                StoreId = storeID,
                TotalPrice = subtotal
            };

            db.Orders.Add(order);
            db.SaveChanges();
            int orderID = order.Id;

            // store each pizza order and count duplicate pizzas
            Dictionary<Pizzas, int> pizzaCount = new Dictionary<Pizzas, int>();
            foreach (Pizzas p in orderedPizzas.Keys)
            {

                Pizzas pizzaOnlyIds = new Pizzas()
                {
                    CrustId = p.Crust.Id,
                    CheeseId = p.Cheese.Id,
                    SauceId = p.Sauce.Id,
                    SizeId = p.Size.Id,
                    Topping1Id = p.Topping1.Id,
                    Topping2Id = p.Topping2.Id,
                };

                if (pizzaCount.ContainsKey(pizzaOnlyIds))
                {
                    // already in list -> increase count
                    pizzaCount[pizzaOnlyIds]++;
                }
                else
                {
                    // add to list
                    pizzaCount.Add(pizzaOnlyIds, 1);
                }
            }

            // add new pizzas to pizza db
            foreach (var item in pizzaCount)
            {
                Pizzas piz = item.Key;
                int count = item.Value;
                // check if has duplicate
                bool hasDuplicate = db.Pizzas.Any(p => (p.SauceId == piz.SauceId &&
                                                    p.CrustId == piz.CrustId &&
                                                    p.CheeseId == piz.CheeseId &&
                                                    p.SizeId == piz.SizeId) &&
                                                    ((p.Topping1Id == piz.Topping1Id && p.Topping2Id == piz.Topping2Id) || (p.Topping1Id == piz.Topping2Id && p.Topping2Id == piz.Topping1Id))
                                                    );

                if (!hasDuplicate)
                {
                    // no duplicate -> add new pizza
                    db.Pizzas.Add(piz);
                    db.SaveChanges();
                    int newPizzaID = piz.Id;

                    db.OrderPizzas.Add(new OrderPizzas()
                    {
                        OrderId = orderID,
                        PizzaId = newPizzaID,
                        Count = count
                    });
                    db.SaveChanges();

                }
                else
                {
                    // duplicate
                    int dupPizzaID = db.Pizzas.Where(p => (p.SauceId == piz.SauceId &&
                                                    p.CrustId == piz.CrustId &&
                                                    p.CheeseId == piz.CheeseId &&
                                                    p.SizeId == piz.SizeId) &&
                                                    ((p.Topping1Id == piz.Topping1Id && p.Topping2Id == piz.Topping2Id) || (p.Topping1Id == piz.Topping2Id && p.Topping2Id == piz.Topping1Id))
                                                    ).Select(p => p.Id).FirstOrDefault();

                    db.OrderPizzas.Add(new OrderPizzas()
                    {
                        OrderId = orderID,
                        PizzaId = dupPizzaID,
                        Count = count
                    });
                    db.SaveChanges();
                }

            }
            return true;
        }

        public List<Orders> GetStoreOrderHistoryById(int storeID)
        {
            var query = db.Orders.Where(o => o.StoreId == storeID);

            return query.ToList();
        }

        public List<Pizzas> GetOrderedPizzasByOrderId(int orderID)
        {
            var query = from p in db.Pizzas
                        from op in db.OrderPizzas
                        where op.OrderId == orderID
                        select p;

            return query.ToList();
        }
    }
}
