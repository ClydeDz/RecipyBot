using RecipyBotWeb.Models;
using RecipyBotWeb.Service;
using System;
using System.Diagnostics;
using System.Web.Mvc;
using RecipyBotWeb.Constants;

namespace RecipyBotWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.WebChatSecret = BotConstants.BotApiSettings.WebChatSecret;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
        
        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactUsDataModel contactUsFormData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EmailService.SendContactForm(contactUsFormData);
                    return View("ContactSuccess");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Contact page post exception message - " + e.Message);
                    return View("Error");
                }
            }
            return View();
        }

        
    }
}