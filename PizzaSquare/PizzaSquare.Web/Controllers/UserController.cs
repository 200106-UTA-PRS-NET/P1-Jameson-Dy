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
        public IActionResult Info(int? id)
        {
            var currUser = _repo.GetCurrUser();

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
            var currUser = _repo.GetCurrUser();

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

    }
}