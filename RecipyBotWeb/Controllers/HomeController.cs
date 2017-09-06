using System.Linq;
using System.Web.Mvc;

namespace RecipyBotWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            string[] food = new string[]{"abc","def", "ghi"};
            string foodD = string.Join(",",  food.Select(row => row).ToArray());
            ViewBag.Message = "Your application description page." + foodD;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Privacy()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Terms()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}