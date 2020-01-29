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

        public UserController(IUserRepo repo)
        {
            _repo = repo;
        }


        [Route("User")]
        [Route("User/Info")]
        public IActionResult Info(int id)
        {
            if (_repo.GetCurrUser() == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var user = _repo.GetUserByID(id);

            UserViewModel uvm = new UserViewModel()
            {
                Name = user.Name,
                Username = user.Username
            };

            return View(uvm);
        }

        public IActionResult Login()
        {
            return View();
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

    }
}