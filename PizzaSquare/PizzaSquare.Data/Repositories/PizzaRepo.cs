using PizzaSquare.Lib;
using PizzaSquare.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PizzaSquare.Data.Models;

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

        public decimal GetPriceByID(int id)
        {
            var pizza = db.Pizzas.Where(p => p.Id == id).Single();

            decimal crust = db.Crusts.Where(c => c.Id == pizza.CrustId).Select(c => c.Price).Single().Value;
            decimal sauce = db.Sauces.Where(s => s.Id == pizza.SauceId).Select(s => s.Price).Single().Value;
            decimal cheese = db.Cheeses.Where(c => c.Id == pizza.CheeseId).Select(c => c.Price).Single().Value;
            decimal size = db.Sizes.Where(s => s.Id == pizza.SizeId).Select(s => s.Price).Single().Value;

            return crust + sauce + cheese + size;
        }

        public PizzaTypeModel GetPizzaTypeModel()
        {
            return new PizzaTypeModel()
            {
                Cheeses = db.Cheeses.Select(c => c).ToList(),
                Sauces = db.Sauces.Select(s => s).ToList(),
                Crusts = db.Crusts.Select(c => c).ToList(),
                Sizes = db.Sizes.Select(s => s).ToList()
            };
        }

    }
}
