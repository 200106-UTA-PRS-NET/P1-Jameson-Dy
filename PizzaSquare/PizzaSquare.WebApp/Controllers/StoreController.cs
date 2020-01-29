using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaSquare.Lib.Interfaces;
using PizzaSquare.Web.Models;

namespace PizzaSquare.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreRepo _repo;
        private readonly IPizzaRepo _pizzaRepo;

        public StoreController(IStoreRepo repo, IPizzaRepo pizzaRepo)
        {
            _repo = repo;
            _pizzaRepo = pizzaRepo;

        }

        public IActionResult Index()
        {
            var stores = _repo.GetStores();
            List<StoreViewModel> svm = new List<StoreViewModel>();
            foreach(var item in stores)
            {
                StoreViewModel store = new StoreViewModel()
                {
                    Name = item.Name,
                    Id = item.Id
                };
                svm.Add(store);
            }
            return View(svm);
        }
        
        // Page to create custom pizzas
        public IActionResult Custom()
        {
            CustomPizzaViewModel customPizzaModel = new CustomPizzaViewModel()
            {
                Cheeses = _pizzaRepo.GetCheeseTypes(),
                Sauces = _pizzaRepo.GetSauceTypes(),
                Crusts = _pizzaRepo.GetCrustTypes(),
                Sizes = _pizzaRepo.GetSizeTypes()
            };

            return View(customPizzaModel);
        }

        // Displays store preset pizzas
        public IActionResult Menu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = _repo.GetStorePizzasById(id.Value);
            List<StorePizzaViewModel> spvm = new List<StorePizzaViewModel>();
            foreach(var p in pizzas)
            {
                StorePizzaViewModel pizza = new StorePizzaViewModel()
                {
                    StoreID = id.Value,
                    PizzaID = p.Id,
                    StoreName = _repo.GetStoreById(id.Value).Name,
                    PizzaName = p.Name,
                    Crust = p.Crust.Name,
                    Sauce = p.Sauce.Name,
                    Cheese = p.Cheese.Name,
                    Toppings = new List<String>() { p.Topping1.Name, p.Topping2.Name },
                    Price = _pizzaRepo.GetPriceByID(p.Id)
                    
                };
                spvm.Add(pizza);
            }

            return View(spvm);
        }
    }
}