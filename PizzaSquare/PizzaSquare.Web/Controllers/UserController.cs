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
    public class UserController : Controller
    {
        private readonly IUserRepo _repo;
        private readonly IOrderRepo _orderRepo;
        private readonly IStoreRepo _storeRepo;

        Users currUser;

        public UserController(IUserRepo repo, IOrderRepo orderRepo, IStoreRepo storeRepo)
        {
            _repo = repo;
            _orderRepo = orderRepo;
            _storeRepo = storeRepo;
            currUser = _repo.GetCurrUser();
        }


        [Route("User")]
        [Route("User/Info")]
        public IActionResult Info(int? id)
        {
            if (currUser == null)
            {
                // not logged in -> send to login page
                return RedirectToAction(nameof(Login));
            }
            
            // assert: logged in

            //var user = _repo.GetUserByID(id.Value);

            UserViewModel uvm = new UserViewModel()
            {
                Name = currUser.Name,
                Username = currUser.Username
            };

            return View(uvm);
        }

        public IActionResult Logout()
        {
            _repo.Logout();
            return Redirect(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUserViewModel user)
        {
            if (currUser != null)
            {
                // someone already logged in
                return RedirectToAction("Info", currUser.Id);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    Users emp = new Users()
                    {
                        Username = user.Username,
                        Password = user.Password
                    };

                    if (_repo.Login(emp))
                    {
                        // login success
                        return RedirectToAction(nameof(Info),1);
                    }

                    return Login();
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    Users emp = new Users()
                    {
                        Name = user.Name,
                        Username = user.Username,
                        Password = user.Password
                    };

                    _repo.AddUser(emp);

                    return RedirectToAction("Login");
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        public IActionResult OrderHistory()
        {
            if (currUser == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var orders = _orderRepo.GetUserOrderHistoryById(currUser.Id);

            List<UserOrderHistoryViewModel> orderHistoryList = new List<UserOrderHistoryViewModel>();
            foreach(Orders o in orders)
            {
                var pizzas = _orderRepo.GetOrderedPizzasByOrderId(o.Id).ToList();

                UserOrderHistoryViewModel uohVM = new UserOrderHistoryViewModel()
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate.Value,
                    StoreName = _storeRepo.GetStoreById(o.StoreId.Value).Name,
                    Subtotal = o.TotalPrice.Value,
                    Pizzas = pizzas
                };
                orderHistoryList.Add(uohVM);
            }

            return View(orderHistoryList);

        }

    }
}