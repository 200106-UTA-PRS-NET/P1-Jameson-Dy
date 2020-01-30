using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaSquare.Lib;
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
            Dictionary<Toppings, bool> top = new Dictionary<Toppings, bool>();
            foreach(var topping in _pizzaRepo.GetToppingTypes())
            {
                top.Add(topping, false);
            }

            CustomPizzaViewModel customPizzaModel = new CustomPizzaViewModel()
            {
                SelCheeseId = 1,
                SelCrustId = 1,
                SelSauceId = 1,
                SelSizeId = 1,
                SelTopping1Id = 1,
                SelTopping2Id = 2,
            };

            ViewData["cheeses"] = _pizzaRepo.GetCheeseTypes();
            ViewData["sauces"] = _pizzaRepo.GetSauceTypes();
            ViewData["crusts"] = _pizzaRepo.GetCrustTypes();
            ViewData["sizes"] = _pizzaRepo.GetSizeTypes();
            ViewData["toppings"] = _pizzaRepo.GetToppingTypes();

            return View(customPizzaModel);
        }
       
        [HttpPost]
        public IActionResult ConfirmAddPizzaToOrder(CustomPizzaViewModel c)
        {
            Pizzas pizza = _pizzaRepo.MapPizzaByIDs(c.SelCrustId, c.SelSauceId, c.SelCheeseId, c.SelSizeId, c.SelTopping1Id, c.SelTopping2Id);

            PizzaViewModel pvm = new PizzaViewModel()
            {
                Cheese = pizza.Cheese.Name,
                Sauce = pizza.Sauce.Name,
                Size = pizza.Size.Name,
                Crust = pizza.Crust.Name,
                Topping1 = pizza.Topping1.Name,
                Topping2 = pizza.Topping2.Name
            };

            return View(pvm);

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