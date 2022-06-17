using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

using static System.Net.WebRequestMethods;
using BCryptNet = BCrypt.Net.BCrypt;

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
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user")); //maybe descending sur le order by
            var listfav = Service.HistoriqueManager.GetAll().Where(x => x.IdPersonne == userConnected.Id).GroupBy(x => x.IdPersonne).ToList();
            Service.HistoriqueManager.Dispose();

            if (listfav.Count > 0)
            {
                List<int> moy = new List<int>();

                foreach (var item in listfav)
                {
                    moy.Add(item.Count());
                }

                ViewBag.moy = moy.Average().ToString();
            }
            else
            {
                ViewBag.moy =  "Pas calculable";
            }

           
            var listcree = Service.RessourcesManager.GetAll().Where(x => x.IdPersonne == userConnected.Id).GroupBy(x => x.IdPersonne).ToList();
            Service.RessourcesManager.Dispose();
            List<int> moye = new List<int>();

            foreach (var item in listcree)
            {
                moye.Add(item.Count());
            }

            if(moye.Count > 0)
            {
                ViewBag.cree = moye.Average().ToString();
            }
            else
            {
                ViewBag.cree = "Pas calculable";
            }
     
            return View();
        }

        public ActionResult Historique()
        {
            return View();
        }
        public ActionResult Favoris()
        {

            return View();
        }
        public JsonResult GetFavoris()
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
            List<Ressources> listressource = new List<Ressources>();
            List<Favori> listfav = Service.FavoriManager.GetByPersonne(userConnected.Id);
            Service.FavoriManager.Dispose();

            foreach (Favori item in listfav)
            {
                listressource.Add(Service.RessourcesManager.Get(item.IdRessource));
                Service.FavoriManager.Dispose();
            }

            return Json(listressource);
        }

        public JsonResult GetHistorique()
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user")); //maybe descending sur le order by
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
        public JsonResult GetAllressource()
        {
            List<Ressources> listressource = Service.RessourcesManager.GetAll().ToList();
            Service.RessourcesManager.Dispose();
            return Json(listressource);
        }
        public JsonResult GetAllressourcetovalidate()
        {
            List<Ressources> listressource = Service.RessourcesManager.Getallfalse().ToList();
            Service.RessourcesManager.Dispose();
            return Json(listressource);
        }
        public ActionResult Getmenu() //menu des filtre affichage de la vue partielle
        {
            return View("/Views/Shared/_FiltresRessources.cshtml");
        }

        [HttpPost]
        public JsonResult GetAllressourceFiltreby(int? idcat, int? idtype, string search)
        {
            List<Ressources> listressource = Service.RessourcesManager.GetAll().Where(x => x.IsValidate == true).ToList();
            Service.RessourcesManager.Dispose();
            // cas all
            if ((idcat != null) && (idtype != null) && (search != null)) //cas ou tous est remplis
            {
                listressource = listressource.Where(x => x.IdCategorie == idcat && x.IdType == idtype && x.Nom.Contains(search)).ToList();
            }

            //cas chiant

            if ((idcat == null) && (idtype != null) && (search != null)) // cas ou il manque le idcat
            {
                listressource = listressource.Where(x => x.IdType == idtype && x.Nom.Contains(search)).ToList();
            }

            if ((idcat != null) && (idtype == null) && (search != null)) //cas ou il manque le idtype
            {
                listressource = listressource.Where(x => x.IdCategorie == idcat && x.Nom.Contains(search)).ToList();
            }

            if ((idcat != null) && (idtype != null) && (search == null)) //cas ou il manque le search
            {
                listressource = listressource.Where(x => x.IdCategorie == idcat && x.IdType == idtype).ToList();
            }

            // cas unitaire

            if ((idcat != null) && (idtype == null) && (search == null)) //cas ou il est présent uniquement le idcat
            {
                listressource = listressource.Where(x => x.IdCategorie == idcat).ToList();
            }

            if ((idcat == null) && (idtype != null) && (search == null)) //cas ou il est présent uniquement le idtype
            {
                listressource = listressource.Where(x => x.IdType == idtype).ToList();
            }

            if ((idcat == null) && (idtype == null) && (search != null)) //cas ou il est présent uniquement le search
            {
                listressource = listressource.Where(x => x.Nom.Contains(search)).ToList();
            }

            return Json(listressource);
        }
        public int Addfav(int id)
        {
            try
            {
                Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
                //on delete pour ne pas avoir de doublon
                Favori oldfav = Service.FavoriManager.Get(userConnected.Id, id);
                Service.FavoriManager.Dispose();
                if (oldfav != null)
                {
                    Service.FavoriManager.DeleteObj(oldfav);
                    Service.FavoriManager.Dispose();
                    return 0;
                }

                Favori fav = new Favori();
                fav.IdPersonne = userConnected.Id;
                fav.IdRessource = id;
                Service.FavoriManager.Add(fav);
                Service.FavoriManager.Dispose();
                return 1;
            }
            catch
            {
                return 99;
            }
        }
        
        public JsonResult Mesressources(int id) //fonction qui va retourner les ressources de la personne where id personne 
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));

            List<Favori> mesfavoris = new List<Favori>();

            mesfavoris = (List<Favori>)Service.FavoriManager.GetByPersonne(userConnected.Id);
            Service.FavoriManager.Dispose();

            List<Ressources> mesressources = (List<Ressources>)Service.RessourcesManager.GetAll().Where(x => x.IdPersonne == (userConnected.Id)).ToList();
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

            }
            return Json(mesressources);
        }
        public JsonResult RessourceAccueil() //fonction qui va retourner les ressources sur la page d'acceuil
        {

            if (HttpContext.Session.GetString("user") != null)
            {
                Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
                List<Favori> mesfavoris = new List<Favori>();

                mesfavoris = (List<Favori>)Service.FavoriManager.GetByPersonne(userConnected.Id);
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

        // GET: RessourceController/Details/5
        public ActionResult Details(int id) //affichera la ressources sur une page a part 
        {
            Ressources maressource = Service.RessourcesManager.Get(id);
            Service.RessourcesManager.Dispose();
            return View(maressource);
        }
        [HttpGet]
        public JsonResult getressourcebycat() //affichera la ressources sur une page a part 
        {

            List<Ressources> newlistressoruce = Service.RessourcesManager.GetAll().ToList();
            Service.RessourcesManager.Dispose();
            List<Categorie> newlistcategorie = Service.CategorieManager.GetAll().ToList();
            Service.CategorieManager.Dispose();

            List<HighChartHisto.HistoBar> serial = new List<HighChartHisto.HistoBar>();

            for (int i = 0; i < newlistcategorie.Count; i++) // 2 correspond au premier id de categorie
            {
                if (newlistressoruce.Any(x => x.IdCategorie == newlistcategorie[i].Id))
                {
                    HighChartHisto.HistoBar serie = new HighChartHisto.HistoBar()
                    {
                        Name = newlistcategorie.FirstOrDefault(x => x.Id == newlistcategorie[i].Id).Libelle,
                        value = newlistressoruce.Count(x => x.IdCategorie == newlistcategorie[i].Id)
                    };

                    serial.Add(serie);
                }
            }


            return Json(serial);
        }

        [HttpGet]
        public JsonResult getressourcebymonth() //affichera la ressources sur une page a part 
        {

            List<Ressources> newlistressoruce = Service.RessourcesManager.GetAll().ToList();
            Service.RessourcesManager.Dispose();
            List<HighchartBar> serial = new List<HighchartBar>();
            List<int> ressbymonth = new List<int>();
            for (int i = 1; i < 12; i++)
            {
                ressbymonth.Add(newlistressoruce.Where(x => x.Date >= new DateTime(2022, i, 01) && x.Date <= new DateTime(2022, i + 1, 01)).Count());
            }

            return Json(ressbymonth);
        }


        // GET: RessourceController/Create
        public ActionResult Create()
        {
            IDictionary<int, string> ListCategories = new Dictionary<int, string>();
            IEnumerable<Categorie> categorie = Service.CategorieManager.GetAll();
            Service.CategorieManager.Dispose();
            foreach (var item in categorie)
            {
                ListCategories.Add(item.Id, $"{item.Libelle}");
            }
            var selectListcat = new SelectList(ListCategories, "Key", "Value");

            ViewBag.ListCategories = selectListcat;

            IDictionary<int, string> ListType = new Dictionary<int, string>();
            IEnumerable<ServiceDAL.BusinessObjet.Type> types = Service.TypeManager.GetAll();
            Service.TypeManager.Dispose();
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
        public async Task<IActionResult> Create(Ressources ressource, IFormFile file)
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
            ressource.Date = System.DateTime.Now;
            ressource.IsValidate = false;
            ressource.IdPersonne = userConnected.Id;

            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);


                string extensstion = fileName.Split(".").Last().ToLower(); //get l'extenstion du docuement

                switch (extensstion)
                {
                    case "docx": //cas word
                        ressource.IdType = 3;
                        break;

                    case "doc": //cas word
                        ressource.IdType = 3;
                        break;

                    case "txt": //cas word
                        ressource.IdType = 3;
                        break;

                    case "mp4": //cas vidéo
                        ressource.IdType = 5;
                        break;

                    case "png": //cas image
                        ressource.IdType = 6;
                        break;

                    case "jpg": //cas image
                        ressource.IdType = 6;
                        break;

                    case "xlsx": //cas excel
                        ressource.IdType = 2;
                        break;

                    case "xls": //cas excel
                        ressource.IdType = 2;
                        break;

                    case "pdf": //cas pdf
                        ressource.IdType = 4;
                        break;

                    default: //cas ou c'est la merde
                        ressource.IdType = 7;
                        break;
                }


                var fileNewName = fileName + "_" + Guid.NewGuid().ToString() + "." + extensstion;
                var path = Path.Combine("./wwwroot/PDF_FOLDER/", fileNewName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                ressource.CheminAcces = "/PDF_FOLDER/";
                ressource.Source = fileNewName;


                Service.RessourcesManager.Add(ressource);
                Service.RessourcesManager.Dispose();
                return Redirect("/Ressource");
            }
            else
            {
                //faire le message d'erreur visuellement
                ViewBag.message = "Il n'y a pas de document lié a votre ressource.";
                return Redirect("/Ressource/Create");
            }

        }


        // GET: RessourceController/Edit/5
        public ActionResult Edit(int id)
        {
            Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
            IDictionary<int, string> ListCategories = new Dictionary<int, string>();
            IEnumerable<Categorie> categorie = Service.CategorieManager.GetAll();
            Service.CategorieManager.Dispose();
            foreach (var item in categorie)
            {
                ListCategories.Add(item.Id, $"{item.Libelle}");
            }
            var selectListcat = new SelectList(ListCategories, "Key", "Value");

            ViewBag.ListCategories = selectListcat;

            Ressources ressource = Service.RessourcesManager.Get(id);
            Service.RessourcesManager.Dispose();

            return View("Edit", ressource);
        }

        // POST: RessourceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ressources ressource, IFormFile file)
        {
            
                Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user"));
                
                Ressources ressourcetosend = new Ressources();


                if (ressource != null)
                {

                    ressourcetosend = Service.RessourcesManager.Get(ressource.Id);
                    Service.RessourcesManager.Dispose();
                    ressourcetosend.Nom = ressource.Nom;
                    ressourcetosend.IdCategorie = ressource.IdCategorie;


                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);


                        string extensstion = fileName.Split(".").Last().ToLower(); //get l'extenstion du docuement

                        switch (extensstion)
                        {
                            case "docx": //cas word
                            ressourcetosend.IdType = 3;
                                break;

                            case "doc": //cas word
                            ressourcetosend.IdType = 3;
                                break;

                            case "txt": //cas word
                            ressourcetosend.IdType = 3;
                                break;

                            case "mp4": //cas vidéo
                            ressourcetosend.IdType = 5;
                                break;

                            case "png": //cas image
                            ressourcetosend.IdType = 6;
                                break;

                            case "jpg": //cas image
                            ressourcetosend.IdType = 6;
                                break;

                            case "xlsx": //cas excel
                            ressourcetosend.IdType = 2;
                                break;

                            case "xls": //cas excel
                            ressourcetosend.IdType = 2;
                                break;

                            case "pdf": //cas pdf
                            ressourcetosend.IdType = 4;
                                break;

                            default: //cas ou c'est la merde
                            ressourcetosend.IdType = 7;
                                break;
                        }


                        var fileNewName = fileName + "_" + Guid.NewGuid().ToString() + "." + extensstion;
                        var path = Path.Combine("./wwwroot/PDF_FOLDER/", fileNewName);
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        using (Stream fileStream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    ressourcetosend.CheminAcces = "/PDF_FOLDER/";
                    ressourcetosend.Source = fileNewName;
                    }

                Service.RessourcesManager.Update(ressourcetosend);
                Service.RessourcesManager.Dispose();
            }

            return Redirect("/Ressource");
        }
          
        [HttpPost]
        public string Delete(int id)
        {
            Service.RessourcesManager.Delete(id);
            Service.RessourcesManager.Dispose();
            return "";

        }
        public ActionResult GetRessource(int id)
        {
            Ressources ressource = Service.RessourcesManager.Get(id); //récupération de la ressource
            Service.RessourcesManager.Dispose();
            if (HttpContext.Session.GetString("user") != null) //si le user est non null
            {
                Personne userConnected = JsonSerializer.Deserialize<Personne>(HttpContext.Session.GetString("user")); // récupération de la persone
                Historique histoold = Service.HistoriqueManager.Get(userConnected.Id, ressource.Id); //recupératon du l'historique
                Service.HistoriqueManager.Dispose();
                if (histoold == null)
                {
                    Historique histotamere = new Historique();
                    histotamere.IdPersonne = userConnected.Id;
                    histotamere.IdRessource = ressource.Id;
                    histotamere.Date = DateTime.Now;

                    Service.HistoriqueManager.Add(histotamere);
                    Service.HistoriqueManager.Dispose();
                }
                else
                {
                    Service.HistoriqueManager.Delete(userConnected.Id, histoold.IdRessource);
                    Service.HistoriqueManager.Dispose();
                    Historique histotamere = new Historique();
                    histotamere.IdPersonne = userConnected.Id;
                    histotamere.IdRessource = ressource.Id;
                    histotamere.Date = DateTime.Now;

                    Service.HistoriqueManager.Add(histotamere);
                    Service.HistoriqueManager.Dispose();
                }
            }

            switch (ressource.IdType)
            {
                case 2: //cas excel
                    return View("RessourceExcel", ressource);
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
