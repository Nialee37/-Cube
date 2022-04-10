using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        // GET: ContactController
        public ActionResult ContactezNous()
        {
            return View();
        }

        // POST: Contact/EnvoieMailContact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnvoieMailContact(string nom, string email, string objet, string textMail)
        {
            if(nom != "" && email != "" && objet != "" && textMail != "")
            {
                //Instanciation du client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 465);
                //On indique au client d'utiliser les informations qu'on va lui fournir
                smtpClient.UseDefaultCredentials = true;
                //Ajout des informations de connexion
                smtpClient.Credentials = new System.Net.NetworkCredential("projetcube.tech@gmail.com", "5$@b&&36p2p$^Z3sZ9");
                //On indique que l'on envoie le mail par le réseau
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //On active le protocole SSL
                smtpClient.EnableSsl = true;

                MailMessage mail = new MailMessage();
                //Expéditeur
                mail.From = new MailAddress(email, nom);
                //Destinataire
                mail.To.Add(new MailAddress("projetcube.tech@gmail.com"));
                //Copie
                mail.Subject = objet;

                mail.Body = textMail;

                smtpClient.Send(mail);

                return RedirectToAction("ContactezNous", "Contact");
            }
            else
            {
                TempData["messageErreurEvoieMail"] = "Il faut completer tous les champs avant d'envoyer votre mail ! ";
                TempData["dataMessageMail"] = textMail;
                TempData["dataOBJMail"] = objet;
                TempData["dataMailForMail"] = email;
                TempData["dataNomMail"] = nom;

                return RedirectToAction("ContactezNous", "Contact");
            }
        }
    }
}
