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

        public OrderController(IOrderRepo repo, IPizzaRepo pizzaRepo, IStoreRepo storeRepo, IUserRepo userRepo)
        {
            _repo = repo;
            _pizzaRepo = pizzaRepo;
            _storeRepo = storeRepo;
            _userRepo = userRepo;
        }

        public IActionResult ViewOrder()
        {
            var pizzas = _repo.GetOrderedPizzas().ToList();

            OrderViewModel ovm = new OrderViewModel()
            {
                Pizzas = new Dictionary<Pizzas, decimal>()
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

        public string SubmitOrder()
        {
            if (_repo.SubmitOrder(_userRepo.GetCurrUser().Id, _storeRepo.GetCurrStore().Id))
            {
                // :)
                return ":D";

            }
            else 
            {
                // :(
                return ":((";
            }
        }
    }
}