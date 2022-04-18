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
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebApp.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly ILogger<AdministrationController> _logger;
        private readonly IService Service;

        public AdministrationController(ILogger<AdministrationController> logger, IService service)
        {
            _logger = logger;
            Service = service;
        }

        public ActionResult GestionRessources()
        {
           /* List<Ressources> listressource = Service.RessourcesManager.Getallfalse().ToList();
            Service.RessourcesManager.Dispose();
            ViewBag.listRessourceFalse = listressource.ToArray();*/

            List<Ressources> listressourceAll = Service.RessourcesManager.GetAll().ToList();
            Service.RessourcesManager.Dispose();
            ViewBag.listAllRessource = listressourceAll.ToArray();

            return View();
        }

        public ActionResult GestionCategoriesEtVilles()
        {
            List<Ville> listVille = Service.VilleManager.GetAll().ToList();
            Service.VilleManager.Dispose();
            ViewBag.listVille = listVille.ToArray();

            List<Categorie> listcategorie = Service.CategorieManager.GetAll().ToList();
            Service.CategorieManager.Dispose();
            ViewBag.listCategorie = listcategorie.ToArray();

            return View();
        }
        public ActionResult StatistiquesGeneral()
        {
            var ressourcelist = Service.RessourcesManager.GetAll().GroupBy(x => x.IdPersonne).ToList();
            Service.RessourcesManager.Dispose();

            if (ressourcelist.Count > 0)
            {
                List<int> moy = new List<int>();

                foreach (var item in ressourcelist)
                {
                    moy.Add(item.Count());
                }

                ViewBag.moycree = Math.Round(moy.Average(), 0).ToString();
            }
            else
            {
                ViewBag.moycree = "Pas calculable";
            }
            var listfav = Service.HistoriqueManager.GetAll().GroupBy(x => x.IdPersonne).ToList();
            Service.HistoriqueManager.Dispose();

            if (listfav.Count > 0)
            {
                List<int> moy = new List<int>();

                foreach (var item in listfav)
                {
                    moy.Add(item.Count());
                }

                ViewBag.moylu = Math.Round(moy.Average(), 0).ToString();
            }
            else
            {
                ViewBag.moylu = "";
            }

            return View();
        }

        public ActionResult GestionUtilisateurs()
        {
            List<Personne> personnes = (List<Personne>)Service.PersonneManager.GetAll();
            Service.PersonneManager.Dispose();
            ViewBag.listPersonne = personnes.ToArray();

            return View();
        }
    }
}
