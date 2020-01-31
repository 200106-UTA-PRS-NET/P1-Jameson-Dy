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
        private readonly IOrderRepo _orderRepo;
        private readonly IUserRepo _userRepo;
        Users currUser = new Users();

        public StoreController(IStoreRepo repo, IPizzaRepo pizzaRepo, IOrderRepo orderRepo, IUserRepo userRepo)
        {
            _repo = repo;
            _pizzaRepo = pizzaRepo;
            _orderRepo = orderRepo;
            _userRepo = userRepo;
            currUser = _userRepo.GetCurrUser();
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
            if (currUser == null)
            {
                return RedirectToAction("Login", "User");
            }

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

        /*
         * public IActionResult Preset(int? id)
        {
            if (currUser == null)
            {
                return RedirectToAction("Login", "User");
            }

        }
        */

        [HttpGet]
        public IActionResult ConfirmPizza(CustomPizzaViewModel c)
        {
            if (currUser == null)
            {
                return RedirectToAction("Login", "User");
            }

            Pizzas pizza = _pizzaRepo.MapPizzaByIDs(c.SelCrustId, c.SelSauceId, c.SelCheeseId, c.SelSizeId, c.SelTopping1Id, c.SelTopping2Id);
            _orderRepo.SetCurrentPizza(pizza);

            string pizzaName ="";
            if (pizza.Name == "" || pizza.Name == null)
            {
                pizzaName = "Custom";
            } else
            {
                pizzaName = pizza.Name;
            }
            PizzaViewModel pvm = new PizzaViewModel()
            {
                Name = pizzaName,
                Cheese = pizza.Cheese,
                Sauce = pizza.Sauce,
                Size = pizza.Size,
                Crust = pizza.Crust,
                Topping1 = pizza.Topping1,
                Topping2 = pizza.Topping2,
                Price = _pizzaRepo.GetPriceByPizza(pizza)
            };

            return View(pvm);
        }

        [HttpPost]
        public IActionResult ConfirmPizza(PizzaViewModel pvm)
        {
            if (currUser == null)
            {
                return RedirectToAction("Login", "User");
            }

            Pizzas p = _orderRepo.GetCurrentPizza();
            _orderRepo.AddPizzaToOrder(p, _pizzaRepo.GetPriceByPizza(p));



            return RedirectToAction("ViewOrder", "Order");
        }

        // Displays store order history
        public IActionResult OrderHistory(int? id)
        {
            if (currUser == null)
            {
                return RedirectToAction("Login", "User");
            }

            List<Orders> orders = _orderRepo.GetStoreOrderHistoryById(id.Value);

            List<StoreOrderHistoryViewModel> storeOrdersVM = new List<StoreOrderHistoryViewModel>();

            
            foreach(var item in orders)
            {
                StoreOrderHistoryViewModel oVM = new StoreOrderHistoryViewModel()
                {
                    UserID = item.UserId.Value,
                    OrderID = item.Id,
                    OrderDate = item.OrderDate.Value,
                    Subtotal = item.TotalPrice.Value,
                    OrderedPizzas = _orderRepo.GetOrderedPizzasByOrderId(item.Id)
                };

                storeOrdersVM.Add(oVM);
            }

            return View(storeOrdersVM);
        }

        // Displays store preset pizzas
        public IActionResult Menu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Stores store = _repo.GetStoreById(id.Value);
            _repo.SetCurrStore(store);

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