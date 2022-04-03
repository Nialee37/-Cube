using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    public class VillesController : Controller
    {
        private readonly ILogger<VillesController> _logger;
        private readonly IService Service;

        public VillesController(ILogger<VillesController> logger, IService service)
        {
            _logger = logger;
            Service = service;
        }
        // GET: VillesController
        public ActionResult Index()
        {
            var villes = Service.VilleManager.GetAll();
            return View(villes);
        }

        public JsonResult JsonVille()
        {
            var villes = Service.VilleManager.GetAll();
            return Json(villes);
        }

        public JsonResult GetAllVille()
        {
            List<Ville> listVille = Service.VilleManager.GetAll().ToList();

            return Json(listVille);
        }

        public JsonResult GetByCPOrVille(string data)
        {
            List<Ville> listVille = Service.VilleManager.GetbyCPOrVille(data).ToList();

            return Json(listVille);
        }

        // GET: VillesController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Ville ville = Service.VilleManager.Get((int)id);
            if (ville == null)
            {
                return NotFound();
            }
            return View(Service.VilleManager.Get((int)id));
        }

        // GET: VillesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VillesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("IdVille,Nom,CPostal")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                Service.VilleManager.Add(ville);
            }
            return RedirectToAction("AdminPersonne", "Personne");
        }

        // GET: VillesController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Ville ville = Service.VilleManager.Get((int)id);
            if (ville == null)
            {
                return NotFound();
            }
            return View(ville);
        }

        // POST: VillesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ville ville)
        {
            if (ModelState.IsValid)
            {
                Service.VilleManager.Update(ville);
            }
            return RedirectToAction("AdminPersonne", "Personne");
        }

    
        public ActionResult Delete(int id)
        {
            Service.VilleManager.Delete(id);
            return RedirectToAction("AdminPersonne", "Personne");
        }
    }
}
