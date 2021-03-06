using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BCryptNet = BCrypt.Net.BCrypt;
namespace WebApp.Controllers
{
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IService Service;
        public ApiController(ILogger<ApiController> logger, IService service)
        {
            _logger = logger;
            Service = service;
        }
        public JsonResult RessourceAccueil() //fonction qui va retourner les ressources sur la page d'acceuil
        {

            if (HttpContext.Session.GetString("user") != null)
            {
                Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
                List<Favori> mesfavoris = new List<Favori>();

                mesfavoris = (List<Favori>)Service.FavoriManager.Getal(userConnected.Id);
                Service.FavoriManager.Dispose();

                List<Ressources> mesressources = (List<Ressources>)Service.RessourcesManager.GetAll().Where(x => x.IsValidate == true).OrderBy(x => x.Date).ToList();
                Service.RessourcesManager.Dispose();
                for (int i = 0; i < mesressources.Count; i++)
                {
                    if (mesfavoris.Find(x => x.IdPersonne == userConnected.Id && x.IdRessource == mesressources[i].Id) != null)
                    {
                        mesressources[i].isfav = 1;
                    }
                    else
                    {
                        mesressources[i].isfav = 0;
                    }

                    mesressources[i].fullfiledowload = "https://projetcube.tech/" + mesressources[i].CheminAcces + mesressources[i].Source;
                }

                return Json(mesressources);
            }
            else
            {
                List<Ressources> mesressources = Service.RessourcesManager.GetAll().Where(x => x.IsValidate == true).OrderBy(x => x.Date).ToList();
                Service.RessourcesManager.Dispose();
                for (int i = 0; i < mesressources.Count; i++)
                {
                    mesressources[i].fullfiledowload = "https://projetcube.tech/" + mesressources[i].CheminAcces + mesressources[i].Source;
                }
                return Json(mesressources);
            }

        }
        public JsonResult RessourceFiltred(string libelletype) //fonction qui va retourner les ressources sur la page d'acceuil
        {
        
                List<Ressources> mesressources = (List<Ressources>)Service.RessourcesManager.GetAll().Where(x => x.IsValidate == true).OrderBy(x => x.Date).ToList();
                Service.RessourcesManager.Dispose();
        
                for (int i = 0; i < mesressources.Count; i++)
                {
                    mesressources[i].fullfiledowload = "https://projetcube.tech/" + mesressources[i].CheminAcces + mesressources[i].Source;
                }

                if(libelletype != null)
            {
                mesressources = mesressources.Where(x => x.Type.Libelle == libelletype).ToList();
                return Json(mesressources);
            }
            else
            {
                return Json(mesressources);
            }
                
        }
        public JsonResult Typelist()
        {
            List<Type> listtype = Service.TypeManager.GetAll().ToList();
            Service.TypeManager.Dispose();
            return Json(listtype);
        }  //renvoie une list de type pour l'application mobile
        public JsonResult GetHistoriqueMobile(string id)
        {
            int idtofindpersonne = 0;
            if (id != null)
            {
                idtofindpersonne = int.Parse(id);
                Personne userConnected = Service.PersonneManager.Get(idtofindpersonne); //maybe descending sur le order by
                Service.PersonneManager.Dispose();
                List<Ressources> listressource = new List<Ressources>();
                List<Historique> listfav = Service.HistoriqueManager.Getal(userConnected.Id).OrderBy(x => x.Date).ToList();
                Service.HistoriqueManager.Dispose();

                foreach (Historique item in listfav)
                {
                    listressource.Add(Service.RessourcesManager.Get(item.IdRessource));
                    Service.RessourcesManager.Dispose();
                }

                return Json(listressource);
            }

            return Json("");
        } //renvoie une liste de ressources lié a l'historique d'un utilisateur
        public JsonResult GetFavorisModile(string id)
        {
            int idtofindpersonne = 0;
            if (id != null)
            {
                idtofindpersonne = int.Parse(id);
                Personne userConnected = Service.PersonneManager.Get(idtofindpersonne);
                Service.PersonneManager.Dispose();
                List<Ressources> listressource = new List<Ressources>();
                List<Favori> listfav = Service.FavoriManager.Getal(userConnected.Id);
                Service.FavoriManager.Dispose();

                foreach (Favori item in listfav)
                {
                    listressource.Add(Service.RessourcesManager.Get(item.IdRessource));
                    Service.RessourcesManager.Dispose();
                }

                return Json(listressource);
            }
            return Json("");
        }//renvoie une liste de ressources lié aux favoris d'un utilisateur
        [HttpPost]
        public JsonResult LoginfromMobile(string email, string motdepasse)
        {
            Personne user = Service.PersonneManager.GetByMail(email);
            Service.PersonneManager.Dispose();
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
                }
                else
                {
                    return Json("");
                }

            }
            else
            {
                return Json("");
            }

        }//Login d'un user et renvoie de son profil
    }
}
