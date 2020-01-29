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

        public StoreController(IStoreRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var stores = _repo.GetStores();
            List<StoreViewModel> svm = new List<StoreViewModel>();
            foreach(var item in stores)
            {
                StoreViewModel store = new StoreViewModel()
                {
                    Name = item.Name
                };
                svm.Add(store);
            }
            return View(svm);
        }
    }
}