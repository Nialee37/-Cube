using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    public class RessourceController : Controller
    {
        private readonly ILogger<RessourceController> _logger;
        private readonly IService Service;

        public RessourceController(ILogger<RessourceController> logger, IService service)
        {
            _logger = logger;
            Service = service;
        }
        // GET: RessourceController
        public ActionResult Index() //fonction qui va retourner une page surlaquelle sera dispoible les ressources de la personne et un onglet de création de ressources
        {
            return View();
        }

        public JsonResult Mesressources(/*int id*/) //fonction qui va retourner les ressources de la personne where id personne 
        {
            List<Ressources> mesressources = (List<Ressources>)Service.RessourcesManager.GetAll();
            return Json(mesressources);
        }
        public JsonResult RessourceAccueil() //fonction qui va retourner les ressources sur la page d'acceuil
        {
            List<Ressources> mesressources = (List<Ressources>)Service.RessourcesManager.GetAll();
            return Json(mesressources);
        }

        // GET: RessourceController/Details/5
        public ActionResult Details(int id) //affichera la ressources sur une page a part 
        {
            Ressources maressource = Service.RessourcesManager.Get(id);
            return View(maressource);
        }

        // GET: RessourceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RessourceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RessourceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RessourceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RessourceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RessourceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
