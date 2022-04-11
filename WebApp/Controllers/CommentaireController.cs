using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace WebApp.Controllers
{
    public class CommentaireController : Controller
    {
        private readonly ILogger<CommentaireController> _logger;
        private readonly IService Service;

        public CommentaireController(ILogger<CommentaireController> logger, IService service)
        {
            _logger = logger;
            Service = service;
        }
        // GET: CommentairesController
        public ActionResult Index()
        {
            var Commentaires = Service.CommentaireManager.GetAll();
            Service.CommentaireManager.Dispose();
            return View(Commentaires);
        }

        public JsonResult JsonVille()
        {
            var Commentaires = Service.CommentaireManager.GetAll();
            Service.CommentaireManager.Dispose();
            return Json(Commentaires);
        }

        public JsonResult GetCommentaireBuyRessource(int id)
        {
            List<Commentaire> listVille = Service.CommentaireManager.GetCommentaireBuyRessource(id).ToList();
            Service.CommentaireManager.Dispose();
            return Json(listVille);
        }

        // GET: CommentairesController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Commentaire ville = Service.CommentaireManager.Get((int)id);
            if (ville == null)
            {
                return NotFound();
            }
            Service.CommentaireManager.Dispose();
            return View(ville);
        }

        // GET: CommentairesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentairesController/Create
        [HttpPost]
        public string Create(int id_ressource, string corp,int? idComReponse)
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
            Commentaire commentaire = new Commentaire();
                commentaire.IdRessource = id_ressource;
                commentaire.commentaire = corp;
                commentaire.IdPersonne = userConnected.Id;
                commentaire.date_com = System.DateTime.Now;
                if (idComReponse != null)
                {
                    commentaire.isReponse = true;
                    commentaire.IdCommentaireOrigine = idComReponse;
                }
            Service.CommentaireManager.Add(commentaire);
            Service.CommentaireManager.Dispose();
            return "Commentaire ajouté";
        }

        //// GET: CommentairesController/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //    Ville ville = Service.CommentaireManager.Get((int)id);
        //    if (ville == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ville);
        //}

        //// POST: CommentairesController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Ville ville)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Service.CommentaireManager.Update(ville);
        //    }
        //    return RedirectToAction("AdminPersonne", "Personne");
        //}

        [HttpPost]
        public bool Delete(int id,int idPersonneRessource)
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
            if (userConnected != null)
            {
                if(userConnected.IdRoles >= 3 || idPersonneRessource == userConnected.Id)
                {
                    Service.CommentaireManager.Delete(id);
                    Service.CommentaireManager.Dispose();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
