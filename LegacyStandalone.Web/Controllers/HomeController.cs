using System.Web.Mvc;

namespace LegacyStandalone.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/Help");
        }
    }
}
