using PizzaSquare.Data.Models;
using PizzaSquare.Lib;
using PizzaSquare.Lib.Interfaces;
using System;
using PizzaSquare.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaSquare.Data.Repositories
{
    public class StoreRepo : IStoreRepo
    {
        PizzaSquareContext db;
        static Stores currStore;

        public StoreRepo()
        {
            db = new PizzaSquareContext();
        }
        public StoreRepo(PizzaSquareContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Pizzas> GetStorePizzasById(int id)
        {
            var storePizzaIDs = db.StorePizzas.Where(sp => sp.StoreId == id).Select(sp => sp.PizzaId).ToList();
            var pizzas = db.Pizzas.Where(p => storePizzaIDs.Contains(p.Id)).ToList();

            // set each pizza to have 
            foreach (Pizzas p in pizzas)
            {
                p.Crust = db.Crusts.Where(c => c.Id == p.CrustId).Single();
                p.Cheese = db.Cheeses.Where(c => c.Id == p.CheeseId).Single();
                p.Sauce = db.Sauces.Where(c => c.Id == p.SauceId).Single();
                p.Size = db.Sizes.Where(c => c.Id == p.SizeId).Single();
                p.Topping1 = db.Toppings.Where(c => c.Id == p.Topping1Id).Single();
                p.Topping2 = db.Toppings.Where(c => c.Id == p.Topping2Id).Single();
            }

            return pizzas;
        }

        public Stores GetStoreById(int id)
        {
            var query = db.Stores.Where(s => s.Id == id).Single();
            return query;
        }

        public IEnumerable<Stores> GetStores()
        {
            var query = db.Stores.Select(s => s);
            return query;
        }

        public void SetCurrStore(Stores s)
        {
            currStore = s;
        }

        public Stores GetCurrStore()
        {
            return currStore;
        }

        public DateTime? GetLastOrderDate(int storeID, int userID)
        {
            try
            {
                var lastOrder = db.Orders.Where(o => o.StoreId == storeID && o.UserId == userID)
                    .OrderByDescending(o => o.OrderDate)
                    .FirstOrDefault();

                return lastOrder.OrderDate.Value;
            } catch (InvalidOperationException)
            {
                    return null;
            }
}
    }
}
