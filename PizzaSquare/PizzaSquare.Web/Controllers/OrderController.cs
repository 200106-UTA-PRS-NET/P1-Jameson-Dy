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
    public class OrderController : Controller
    {
        private readonly IOrderRepo _repo;
        private readonly IPizzaRepo _pizzaRepo;
        private readonly IStoreRepo _storeRepo;
        private readonly IUserRepo _userRepo;
        Users currUser;
        Stores currStore;

        public OrderController(IOrderRepo repo, IPizzaRepo pizzaRepo, IStoreRepo storeRepo, IUserRepo userRepo)
        {
            _repo = repo;
            _pizzaRepo = pizzaRepo;
            _storeRepo = storeRepo;
            _userRepo = userRepo;
            currUser = _userRepo.GetCurrUser();
            currStore = _storeRepo.GetCurrStore();
        }

        public IActionResult ViewOrder()
        {
            if (currUser == null)
            {
                return RedirectToAction("Login", "User");
            }

            var pizzas = _repo.GetOrderedPizzas().ToList();

            OrderViewModel ovm = new OrderViewModel()
            {
                Pizzas = new Dictionary<Pizzas, decimal>(),
                StoreID = _storeRepo.GetCurrStore().Id
            };

            decimal subtotal = 0;
            foreach(Pizzas p in pizzas)
            {
                if (p.Name == null || p.Name == "")
                {
                    p.Name = "Custom";
                }
                decimal price = _pizzaRepo.GetPriceByPizza(p);
                subtotal += price;
                ovm.Pizzas.Add(p, price);
            }
            ovm.Subtotal = subtotal;
            return View(ovm);
        }

        public IActionResult SubmitOrder()
        {
            if (currUser == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (_repo.SubmitOrder(_userRepo.GetCurrUser().Id, _storeRepo.GetCurrStore().Id))
            {
                // ORDER SUBMITTED
                _repo.ClearOrder();
                return RedirectToAction("Menu", "Store", new { id = _storeRepo.GetCurrStore().Id });
            }
            else 
            {
                // ORDER NOT SUBMITTED
                _repo.ClearOrder();
                return RedirectToAction("Menu", "StoreController", _storeRepo.GetCurrStore().Id);
            }
        }

        

    }
}