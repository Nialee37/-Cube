using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceDAL.BusinessObjet;
using ServiceDAL.Interfaces;
using BCryptNet = BCrypt.Net.BCrypt;


namespace WebApp.Controllers
{
    public class ConnectionController : Controller
    {
        private readonly IService Service;
        private readonly ILogger<ConnectionController> _logger;
        
        

        public ConnectionController (ILogger<ConnectionController> logger, IService service)
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
            return View();
        }

        [ValidateAntiForgeryToken]
        public IActionResult IndexLogin(string username, string password)
        {

            Personne user = Service.PersonneManager.GetByMail(username);

            if(user != null)
            {
                if(BCryptNet.Verify(password, user.PasswordHash) && password != "")
                {

                    //User.AddIdentity(user); //mise en place de la variable accessible partout dans le code
                    ViewBag.message = "Bonjour " + user.Prenom.ToString();
                    return View("~/Views/Home/Index.cshtml");
                }
                else
                {
                    ViewBag.message = "Votre mot de passe est incorrect";
                    return View("Index");
                }

            }
            else
            {
                ViewBag.message = "Aucun compte n'existe avec ce mail ou ce nom d'utilisateur";
                return View("Index");
            }          
        }

        public IActionResult RegisterCreate(string username, string password)
        {

            // code a recuperer pour la création du compte
            return View();
        }
    }
}
