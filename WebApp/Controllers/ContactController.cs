using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

                SmtpClient smtpClient = getConfigSmtp();

                MailMessage mail = new MailMessage();
                //Expéditeur
                mail.From = new MailAddress(email, nom);
                //Destinataire
                mail.To.Add(new MailAddress(email));
                //Copie
                mail.Subject = objet;

                mail.Body = textMail;

                try
                {
                    smtpClient.Send(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

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
        public SmtpClient getConfigSmtp()
        {
            //Instanciation du client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            //Port SMTP
            smtpClient.Port = 587;
            //On indique au client d'utiliser les informations qu'on va lui fournir
            smtpClient.UseDefaultCredentials = false;
            //Ajout des informations de connexion
            smtpClient.Credentials = new NetworkCredential("projetcube.tech", "5$@b&&36p2p$^Z3sZ9"); ;
            //On indique que l'on envoie le mail par le réseau
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //On active le protocole SSL
            smtpClient.EnableSsl = true;

            return smtpClient;
        }
        public void EnvoieMailBienvenue(string nom, string email)
        {
            if (nom != "" && email != "")
            {

                SmtpClient smtpClient = getConfigSmtp();

                MailMessage mail = new MailMessage();
                //Expéditeur
                mail.From = new MailAddress(email, nom);
                //Destinataire
                mail.To.Add("projetcube.tech@gmail.com");
                //Copie
                mail.Subject = "Bienvenu sur notre site projetcube.tech";

                mail.IsBodyHtml = true;

                mail.Body = BodyRegister(nom);

                try
                {
                    smtpClient.Send(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
        private string BodyRegister(string userName)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("~/ViewsMail/register.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            return body;
        }
    }
}
