using PizzaSquare.Lib;
using PizzaSquare.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaSquare.Data.Repositories
{
    public class PizzaRepo : IPizzaRepo
    {
        PizzaSquareContext db;

        public PizzaRepo()
        {
            db = new PizzaSquareContext();
        }
        public PizzaRepo(PizzaSquareContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public Pizzas MapPizzaByIDs(int crustID, int sauceID, int cheeseID, int sizeID, int topping1ID, int topping2ID)
        {
            return new Pizzas()
            {
                Cheese = db.Cheeses.Where(c => c.Id == cheeseID).Single(),
                Sauce = db.Sauces.Where(s => s.Id == sauceID).Single(),
                Crust = db.Crusts.Where(c => c.Id == crustID).Single(),
                Size = db.Sizes.Where(s => s.Id == sizeID).Single(),
                Topping1 = db.Toppings.Where(t => t.Id == topping1ID).Single(),
                Topping2 = db.Toppings.Where(t => t.Id == topping2ID).Single()
            };
        }

        public decimal GetPriceByID(int id)
        {
            var pizza = db.Pizzas.Where(p => p.Id == id).Single();

            decimal crust = db.Crusts.Where(c => c.Id == pizza.CrustId).Select(c => c.Price).Single().Value;
            decimal sauce = db.Sauces.Where(s => s.Id == pizza.SauceId).Select(s => s.Price).Single().Value;
            decimal cheese = db.Cheeses.Where(c => c.Id == pizza.CheeseId).Select(c => c.Price).Single().Value;
            decimal size = db.Sizes.Where(s => s.Id == pizza.SizeId).Select(s => s.Price).Single().Value;

            return crust + sauce + cheese + size;
        }

        public List<Cheeses> GetCheeseTypes()
        {
            return db.Cheeses.Select(c => c).ToList();
        }

        public List<Crusts> GetCrustTypes()
        {
            return db.Crusts.Select(c => c).ToList();
        }

        public List<Sauces> GetSauceTypes()
        {
            return db.Sauces.Select(c => c).ToList();
        }

        public List<Sizes> GetSizeTypes()
        {
            return db.Sizes.Select(c => c).ToList();
        }

        public List<Toppings> GetToppingTypes()
        {
            return db.Toppings.Select(c => c).ToList();
        }

        public decimal GetPriceByPizza(Pizzas p)
        {
            return p.Cheese.Price.Value + p.Sauce.Price.Value + p.Crust.Price.Value + p.Size.Price.Value;
        }

        public Pizzas GetPizzaById(int id)
        {
            var query = db.Pizzas.Where(p => p.Id == id).Single();

            return query;
        }
    }
}
