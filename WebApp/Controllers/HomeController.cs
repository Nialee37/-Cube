using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService Service;

        public HomeController(ILogger<HomeController> logger, IService service)
        {
            _logger = logger;
            Service = service;
        }

        public IActionResult Index()
        {
            //var test = Service.AdresseManager.Get(1);
            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
