using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceDAL.Interfaces;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Http(int statusCode)
        {
            if (statusCode == 404)
            {
                WebApp.Models.Error erreur = new WebApp.Models.Error(404, "La page que vous recherchez n'existe pas ou n'existe plus !");
                return View("~/Views/Shared/PageError.cshtml", erreur);
            }else
            {
                WebApp.Models.Error erreur = new WebApp.Models.Error(statusCode, "Une erreur est survenue !");
                return View("~/Views/Shared/PageError.cshtml", erreur);
            }
        }
    }
}
