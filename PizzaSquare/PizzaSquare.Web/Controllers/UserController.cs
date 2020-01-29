﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Info(int id=1)
        {
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

    }
}