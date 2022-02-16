﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BCryptNet = BCrypt.Net.BCrypt;

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
            ViewBag.dada = Service.RessourcesManager.Getallfalse();
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
            /*List<Roles> lesRoles = (List<Roles>)Service.RolesManager.GetAll();
            ViewBag.Roles = lesRoles;*/

            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));

            if(userConnected != null && (userConnected.Roles.Libelle == "Administrateur" || userConnected.Roles.Libelle == "SuperAdministrateur") && userConnected.Id != user.Id)
            {
                IDictionary<int, string> ListRoles = new Dictionary<int, string>();
                IEnumerable<Roles> roles = Service.RolesManager.GetAll();
                foreach (var item in roles)
                {
                    ListRoles.Add(item.Id, $"{item.Libelle}");
                }
                var selectListRoles = new SelectList(ListRoles, "Key", "Value");
                var selectedRole = selectListRoles.Where(x => x.Value == user.IdRoles.ToString()).First();
                selectedRole.Selected = true;
                ViewBag.ListRoles = selectListRoles;
            }
            


            IDictionary<int, string> ListVilles = new Dictionary<int, string>();
            IEnumerable<Ville> villes = Service.VilleManager.GetAll().OrderBy(v => v.CPostal);
            //ListVilles.Add(-1, "Veuillez sélectionner une ville");
            foreach (var item in villes)
            {
                ListVilles.Add(item.IdVille, $"<i class='fas fa-building'></i> - {item.CPostal} - {item.Nom}");
            }
            var selectListVille = new SelectList(ListVilles, "Key", "Value");
            var selectedVille = selectListVille.Where(x => x.Value == user.Adresse.IdVille.ToString()).First();
            selectedVille.Selected = true;
            ViewBag.ListVilles = selectListVille;



            IDictionary<int, string> ListGenres = new Dictionary<int, string>();

            string genre = "<i class='fas fa-mars'></i> - Homme,<i class='fas fa-venus'></i> - Femme,<i class='fas fa-genderless'></i> - Autre";
            var temp = genre.Split(",");
            //ListGenres.Add(-1, "Veuillez sélectionner une genre");

            for (int i = 0; i < temp.Count(); i++)
            {
                ListGenres.Add(i, temp[i]);
            }
            var selectListGenre = new SelectList(ListGenres, "Key", "Value");
            var selectedGenre = selectListGenre.Where(x => x.Value == user.Genre.ToString()).First();
            selectedGenre.Selected = true;
            ViewBag.ListGenres = selectListGenre;


            IDictionary<int, string> ListTypeAdresse = new Dictionary<int, string>();

            string type = "Boulevard,Rue,Impace,Avenue,Ruelle,Chemin,Place";
            var temp12 = type.Split(",");
            //ListTypeAdresse.Add(-1, "Veuillez sélectionner un type d'adresse");

            for (int i = 0; i < temp12.Count(); i++)
            {
                ListTypeAdresse.Add(i, temp12[i]);
            }
            var selectListType = new SelectList(ListTypeAdresse, "Key", "Value");
            var selectedType = selectListType.Where(x => x.Value == user.Adresse.Type.ToString()).First();
            selectedType.Selected = true;
            ViewBag.ListTypeAdresse = selectListType;

            return View(user);
        }

        // POST: PersonneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void EditGeneral(Personne user)
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
            Personne getUser = Service.PersonneManager.Get(user.Id);
            getUser.Prenom = user.Prenom;
            getUser.Nom = user.Nom;
            getUser.Genre = user.Genre;
            getUser.DateNaissance = user.DateNaissance;
            Service.PersonneManager.Update(getUser);

            getUser = Service.PersonneManager.Get(getUser.Id);
            if(userConnected.Id == user.Id)
            {
                string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(getUser);
                HttpContext.Session.SetString("user", jsonUser);
            }
            /*string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            HttpContext.Session.SetString("user", jsonUser);*/
            /*List<Roles> lesRoles = (List<Roles>)Service.RolesManager.GetAll();
            ViewBag.Roles = lesRoles;*/
            Response.Redirect("/Personne/Edit?id="+ getUser.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void EditVilleUser(Personne user)
        {
            Personne getUser = Service.PersonneManager.Get(user.Id);
            getUser.Adresse = Service.AdresseManager.Get(getUser.IdAdresse);

            getUser.Adresse.Numero = user.Adresse.Numero;
            getUser.Adresse.Type = user.Adresse.Type;
            getUser.Adresse.Nom = user.Adresse.Nom;
            getUser.Adresse.IdVille = user.Adresse.IdVille;
            getUser.Adresse.Ville = Service.VilleManager.Get(getUser.Adresse.IdVille);

            Service.PersonneManager.Update(getUser);

            getUser = Service.PersonneManager.Get(getUser.Id);
            /*string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            HttpContext.Session.SetString("user", jsonUser);*/
            /*List<Roles> lesRoles = (List<Roles>)Service.RolesManager.GetAll();
            ViewBag.Roles = lesRoles;*/
            Response.Redirect("/Personne/Edit?id="+ getUser.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void EditSecuriter(Personne user)
        {
            Personne getUser = Service.PersonneManager.Get(user.Id);
            if(user.Mail != "" && user.Mail != null){
                if (user.Mail != getUser.Mail)
                {
                    Personne checkMail = Service.PersonneManager.GetByMail(user.Mail);
                    if (checkMail != null)
                    {
                        if (user.Mail == checkMail.Mail)
                        {
                            ViewBag.messageEdit = "Bonjour, un compte existe deja avec cet identifiant !";
                            Response.Redirect("/Personne/Edit?id=" + getUser.Id);
                        }
                        else
                        {
                            getUser.Mail = user.Mail;
                        }
                    }
                    else
                    {
                        getUser.Mail = user.Mail;
                    }
                }
            }
            if (user.PasswordHash != "" && user.PasswordHash != null)
            {
                getUser.PasswordHash = BCryptNet.HashPassword(user.PasswordHash);
            }

            Service.PersonneManager.Update(getUser);
            user = Service.PersonneManager.Get(user.Id);
            /*string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            HttpContext.Session.SetString("user", jsonUser);*/
            /*List<Roles> lesRoles = (List<Roles>)Service.RolesManager.GetAll();
            ViewBag.Roles = lesRoles;*/
            Response.Redirect("/Personne/Edit?id="+ getUser.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void EditRole(Personne user)
        {
            Personne getUser = Service.PersonneManager.Get(user.Id);
            if (user.IdRoles != null && user.IdRoles > 1)
            {
                getUser.IdRoles = user.IdRoles;
            }

            Service.PersonneManager.Update(getUser);
            Response.Redirect("/Personne/Edit?id=" + getUser.Id);
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
