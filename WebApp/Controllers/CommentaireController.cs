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
            return View(Commentaires);
        }

        public JsonResult JsonVille()
        {
            var Commentaires = Service.CommentaireManager.GetAll();
            return Json(Commentaires);
        }

        public JsonResult GetCommentaireBuyRessource(int id)
        {
            List<Commentaire> listVille = Service.CommentaireManager.GetCommentaireBuyRessource(id).ToList();

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
            return View(Service.CommentaireManager.Get((int)id));
        }

        // GET: CommentairesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentairesController/Create
        [HttpPost]
     
        public string Create(int id_ressource, string corp)
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
            Commentaire commentaire = new Commentaire();
                commentaire.IdRessource = id_ressource;
                commentaire.commentaire = corp;
                commentaire.IdPersonne = userConnected.Id;
                Service.CommentaireManager.Add(commentaire);
            
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
        public string Delete(int id)
        {
            Service.CommentaireManager.Delete(id);
            return "commentaire supprimé";
        }
    }
}
