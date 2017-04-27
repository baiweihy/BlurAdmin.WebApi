using System;
using LegacyStandalone.Web.Areas.HelpPage.ModelDescriptions;
using LegacyStandalone.Web.Areas.HelpPage.Models;
using System.Web.Mvc;
using System.Web.Http;

namespace LegacyStandalone.Web.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    [System.Web.Mvc.AllowAnonymous]
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }
        
        public ActionResult Index()
        {
            if (!HttpContext.Request.IsLocal)
            {
                Response.Status = "401.0 - Unauthorized";
                Response.End();
                return new HttpUnauthorizedResult("Unauthorized");
            }
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!HttpContext.Request.IsLocal)
            {
                Response.Status = "401.0 - Unauthorized";
                Response.End();
                return new HttpUnauthorizedResult("Unauthorized");
            }
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!HttpContext.Request.IsLocal)
            {
                Response.Status = "401.0 - Unauthorized";
                Response.End();
                return new HttpUnauthorizedResult("Unauthorized");
            }
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}