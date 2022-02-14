using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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

        public bool Addfav(int id)
        {
            try
            {
                Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
                Favori fav = new Favori();
                fav.IdPersonne = userConnected.Id;
                fav.IdRessource = id;
                Service.FavoriManager.Add(fav);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public JsonResult Mesressources(int id) //fonction qui va retourner les ressources de la personne where id personne 
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));

            List<Ressources> mesressources = (List<Ressources>)Service.RessourcesManager.GetAll().Where(x => x.NomPersonne == (userConnected.Nom +" "+ userConnected.Prenom)).ToList();
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
            IDictionary<int, string> ListCategories = new Dictionary<int, string>();
            IEnumerable<Categorie> categorie = Service.CategorieManager.GetAll();
            foreach (var item in categorie)
            {
                ListCategories.Add(item.Id, $"{item.Libelle}");
            }
            var selectListcat = new SelectList(ListCategories, "Key", "Value");
            
            ViewBag.ListCategories = selectListcat;

            IDictionary<int, string> ListType = new Dictionary<int, string>();
            IEnumerable<ServiceDAL.BusinessObjet.Type> types = Service.TypeManager.GetAll();
            foreach (var item in types)
            {
                ListType.Add(item.Id, $"{item.Libelle}");
            }
            var selectListType = new SelectList(ListType, "Key", "Value");
            ViewBag.ListType = selectListType;

            return View();
        }

        // POST: RessourceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ressources ressource)
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
            ressource.Date = System.DateTime.Today;
            ressource.NomPersonne = userConnected.Nom +" "+ userConnected.Prenom;
            ressource.IsValidate = false;
            Service.RessourcesManager.Add(ressource);

            Ressources ressourcetemp = (Ressources)Service.RessourcesManager.GetAll().OrderByDescending(x => x.Date).FirstOrDefault();
            return Edit(ressourcetemp.Id);
        }

        // GET: RessourceController/Edit/5
        public ActionResult Edit(int id)
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
            
            Ressources ressource = Service.RessourcesManager.Get(id);

            //if((ressource.idCreator == userConnected.id && ressource.validate == false) || (userConnected.Roles.Libelle =="SuperAdministrateur" || userConnected.Roles.Libelle == "Administrateur"))
            //{
            //    return View("Edit", ressource);
            //}
            //return View("index");

            return View("Edit", ressource);
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
        public ActionResult GetRessource(int id)
        {
            Ressources ressource = Service.RessourcesManager.Get(id); //récupération de la ressource

            if (HttpContext.Session.GetString("user") != null) //si le user est non null
            {
                Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user")); // récupération de la persone
                Historique histoold = Service.HistoriqueManager.Get(userConnected.Id, ressource.Id); //recupératon du l'historique

                if(histoold == null)
                {
                    Historique histotamere = new Historique();
                    histotamere.IdPersonne = userConnected.Id;
                    histotamere.IdRessource = ressource.Id;
                    histotamere.Date = DateTime.Now;
                    
                    Service.HistoriqueManager.Add(histotamere);
                } // si c'est null on ajoute sinon on y affiche simplement
            }

            switch (ressource.IdType)
            {
                case 2 : //cas excel
                    return View("RessourceExcel",ressource);
                    break;

                case 3: //cas word
                    return View("RessourceWord", ressource);
                    break;

                case 4: //cas pdf
                    return View("RessourcePDF", ressource);
                    break;

                case 5: //video
                    return View("RessourceVideo", ressource);
                    break;

                case 6: // cas images
                    return View("RessourceIMG", ressource);
                    break;

                default: //cas si ca ne passe pas dans les autres
                    return View(ressource);
                    break;
            }
        }
    }
}
