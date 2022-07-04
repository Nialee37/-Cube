using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ILogger<CategorieController> _logger;
        private readonly IService Service;

        public CategorieController(ILogger<CategorieController> logger, IService service)
        {
            _logger = logger;
            Service = service;
        }

        // GET: VillesController
        [Authorize(Roles = "Modérateur, Administrateur, SuperAdministrateur")]
        public ActionResult Index()
        {
            var villes = Service.CategorieManager.GetAll();
            Service.CategorieManager.Dispose();
            return View(villes);
        }

        // GET: VillesController/Details/5
        [Authorize(Roles = "Modérateur, Administrateur, SuperAdministrateur")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Categorie ville = Service.CategorieManager.Get((int)id);
            if (ville == null)
            {
                return NotFound();
            }
            Service.CategorieManager.Dispose();
            return View(Service.CategorieManager.Get((int)id));
        }

        // GET: VillesController/Create
        [Authorize(Roles = "Modérateur, Administrateur, SuperAdministrateur")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: VillesController/Create
        [Authorize(Roles = "Modérateur, Administrateur, SuperAdministrateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                Service.CategorieManager.Add(categorie);
                Service.CategorieManager.Dispose();
            }
            return RedirectToAction("GestionCategoriesEtVilles", "Administration");
        }
        // GET: VillesController/Edit/5
        [Authorize(Roles = "Modérateur, Administrateur, SuperAdministrateur")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Categorie categorie = Service.CategorieManager.Get((int)id);
            if (categorie == null)
            {
                return NotFound();
            }
            Service.CategorieManager.Dispose();
            return View(categorie);
        }
        // POST: VillesController/Edit/5
        [Authorize(Roles = "Modérateur, Administrateur, SuperAdministrateur")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                Service.CategorieManager.Update(categorie);
                Service.CategorieManager.Dispose();
            }
            return RedirectToAction("GestionCategoriesEtVilles", "Administration");
        }
        public JsonResult GetAllCategorie()
        {
            List<Categorie> listcategorie = Service.CategorieManager.GetAll().ToList();
            Service.CategorieManager.Dispose();

            return Json(listcategorie);
        } //Renvoie toute les catégories

        [Authorize(Roles = "Modérateur, Administrateur, SuperAdministrateur")]
        public ActionResult Delete(int id)
        {
            Service.CategorieManager.Delete(id);
            Service.CategorieManager.Dispose();
            return RedirectToAction("GestionCategoriesEtVilles", "Administration");
        }
    }
}
