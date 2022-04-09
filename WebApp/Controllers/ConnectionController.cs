

using Microsoft.AspNetCore.Http;
            using Microsoft.AspNetCore.Mvc;
            using Microsoft.AspNetCore.Mvc.Rendering;
            using Microsoft.Extensions.Logging;
            using ServiceDAL.BusinessObjet;
            using ServiceDAL.Interfaces;
            using System.Collections.Generic;
            using System.Linq;
            using BCryptNet = BCrypt.Net.BCrypt;


namespace WebApp.Controllers
    {
        public class ConnectionController : Controller
        {
            private readonly IService Service;
            private readonly ILogger<ConnectionController> _logger;



            public ConnectionController(ILogger<ConnectionController> logger, IService service)
            {
                _logger = logger;
                Service = service;
            }
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult Register()
            {


                IDictionary<int, string> ListVilles = new Dictionary<int, string>();
                IEnumerable<Ville> villes = Service.VilleManager.Get30last().OrderBy(v => v.CPostal);
                //ListVilles.Add(-1, "Veuillez sélectionner une ville");
                foreach (var item in villes)
                {
                    ListVilles.Add(item.IdVille, $"<i class='fas fa-building'></i> - {item.CPostal} - {item.Nom}");
                }
                ViewBag.ListVilles = new SelectList(ListVilles, "Key", "Value");



                IDictionary<int, string> ListGenres = new Dictionary<int, string>();

                string genre = "<i class='fas fa-mars'></i> - Homme,<i class='fas fa-venus'></i> - Femme,<i class='fas fa-genderless'></i> - Autre";
                var temp = genre.Split(",");
                //ListGenres.Add(-1, "Veuillez sélectionner une genre");

                for (int i = 0; i < temp.Count(); i++)
                {
                    ListGenres.Add(i, temp[i]);
                }
                ViewBag.ListGenres = new SelectList(ListGenres, "Key", "Value");


                IDictionary<int, string> ListTypeAdresse = new Dictionary<int, string>();

                string type = "Boulevard,Rue,Impace,Avenue,Ruelle,Chemin,Place";
                var temp12 = type.Split(",");
                //ListTypeAdresse.Add(-1, "Veuillez sélectionner un type d'adresse");

                for (int i = 0; i < temp12.Count(); i++)
                {
                    ListTypeAdresse.Add(i, temp12[i]);
                }
                ViewBag.ListTypeAdresse = new SelectList(ListTypeAdresse, "Key", "Value");

                return View();
            }

            [ValidateAntiForgeryToken]
            public IActionResult IndexLogin(string username, string password)
            {
                if(username != null && password != null) {
                    Personne user = Service.PersonneManager.GetByMail(username);

                    if (user != null)
                    {
                        if (user.IsActivate)
                        {
                            if (BCryptNet.Verify(password, user.PasswordHash) && password != "")
                            {

                                //User.AddIdentity(user); //mise en place de la variable accessible partout dans le code
                                ViewBag.message = "Bonjour " + user.Prenom.ToString();

                                Service.PersonneManager.Update(user);
                                string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                                HttpContext.Session.SetString("user", jsonUser);
                                Response.Redirect("/");
                            return RedirectToAction("Index", "Home");
                                //return View("~/Views/Home/Index.cshtml");
                            }
                            else
                            {
                                ViewBag.message = "Votre mot de passe est incorrect";
                                return View("Index");
                            }
                        }else
                        {
                            WebApp.Models.Error erreur = new WebApp.Models.Error(403, "Votre compte a été désactivé,\n veuillez nous contacter à l'adresse source.relationnel@gmail.com,\n pour pouvoir réactiver votre compte");
                            
                            return View("~/Views/Shared/PageError.cshtml", erreur);
                        }
                    }
                    else
                    {
                        ViewBag.message = "Aucun compte n'existe avec ce mail ou ce nom d'utilisateur";
                        return View("Index");
                    }
                }else {
                    ViewBag.message = "Veuillez saisir un mail et un mot de passe !";
                    return View("Index");
            }
            }

            public IActionResult RegisterCreate(Personne user)
            {
            Personne userbdd = Service.PersonneManager.GetByMail(user.Mail);
                
            if (userbdd != null) { 
                if (user.Mail == userbdd.Mail)
                {
                    ViewBag.message = "Bonjour un compte existe deja avec cet identifiant.";
                    return RedirectToAction("Index", "Home");
                }
            }
                string temppsd = user.PasswordHash; //garde du mot de passe en clair pour avoir une connection automatique

                //enregitrement de l'adresse au préalable afin d'avoir l'id
                user.PasswordHash = BCryptNet.HashPassword(user.PasswordHash); //hashage du password pour save en base de donnée
                user.IdRoles = 2; //role normal citoyen connecté
                user.IsActivate = true;
                user.Roles = Service.RolesManager.Get(user.IdRoles);

                Service.PersonneManager.Add(user);
                // code a recuperer pour la création du compte
                return IndexLogin(user.Mail, temppsd);
            }

        public IActionResult Logout()
        {
            Response.Redirect("/");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult LoginfromMobile(string email, string motdepasse)
        {
            Personne user = Service.PersonneManager.GetByMail(email);          
            if (user != null) 
            {
                if (user.IsActivate)
                {
                    if (BCryptNet.Verify(motdepasse, user.PasswordHash) && motdepasse != "")
                    {
                        //Personne newUser = user;
                        //newUser.PasswordHash = "";
                        return Json(user);
                    }
                    else
                    {
                        return Json("");
                    }
                }else
                {
                    return Json("");
                }
                
            }
            else
            {
                return Json("");
            }

        }

    }
}