using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Type = ServiceDAL.BusinessObjet.Type;

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
        public ActionResult Index()
        {
            ViewBag.ListRessources = Service.RessourcesManager.Get10last(); //récupération des 10 derniere.
            Service.RessourcesManager.Dispose();
            ViewBag.Categories = Service.CategorieManager.GetAll();
            Service.CategorieManager.Dispose();
            ViewBag.Type = Service.TypeManager.GetAll();
            Service.TypeManager.Dispose();

            IDictionary<int, string> ListCategories = new Dictionary<int, string>();
            IEnumerable<Categorie> villes = Service.CategorieManager.GetAll();
            Service.CategorieManager.Dispose();
            //ListVilles.Add(-1, "Veuillez sélectionner une ville");
            foreach (var item in villes)
            {
                ListCategories.Add(item.Id, item.Libelle);
            }
            var selectlistcategorie = new SelectList(ListCategories, "Key", "Value");
           
            ViewBag.Categories = selectlistcategorie;

            IDictionary<int, string> ListType = new Dictionary<int, string>();
            IEnumerable<Type> types = Service.TypeManager.GetAll();
            //ListVilles.Add(-1, "Veuillez sélectionner une ville");
            foreach (var item in types)
            {
                ListType.Add(item.Id, item.Libelle);
            }
            var selectlisttype = new SelectList(ListType, "Key", "Value");

            ViewBag.Types = selectlisttype;
            Service.TypeManager.Dispose();

            if (TempData["messageConfirmSupp"] != null)
            {
                ViewBag.message = TempData["messageConfirmSupp"].ToString();
            }

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
