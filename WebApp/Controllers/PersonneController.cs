using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    public class PersonneController : Controller
    {
        private readonly ILogger<PersonneController> _logger;
        private readonly IService Service;

        public PersonneController(ILogger<PersonneController> logger, IService service)
        {
            _logger = logger;
            Service = service;
        }
        // GET: PersonneController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PersonneController/Details/5
        public ActionResult Details(int id)
        {
            

            return View();
        }

        // GET: PersonneController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonneController/Create
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

        // GET: PersonneController/Edit/5
        public ActionResult Edit(int id)
        {

            Personne user = Service.PersonneManager.Get(id);

            return View(user);
        }

        // POST: PersonneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Personne user)
        {
            Service.PersonneManager.Update(user);

            user = Service.PersonneManager.Get(user.Id);
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            HttpContext.Session.SetString("user", jsonUser);
            return View(user);
        }

        // GET: PersonneController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonneController/Delete/5
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

        public ActionResult AdminPersonne()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAllPersonne()
        {
            List<Personne> personnes = (List<Personne>)Service.PersonneManager.GetAll();
            return Json(personnes);
        }
    }
}
