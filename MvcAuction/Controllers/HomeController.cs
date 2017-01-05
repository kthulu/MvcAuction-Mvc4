using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using MvcAuction.Models;

namespace MvcAuction.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [OutputCache(Duration = 3)]
        public ActionResult Index()
        {
            ViewBag.Message = "This page was created " + DateTime.Now;

            return View();
        }
        [OutputCache(Duration=3600)]
        public ActionResult CategoryNavigation()
        {
            var db = new AuctionsDataContext();
            var categories = db.Auctions.Select(x => x.Category).Distinct();
            ViewBag.Categories = categories.ToArray();

            return PartialView();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SwitchView(string returnUrl, bool mobile = false)
        {
            HttpContext.ClearOverriddenBrowser();
            HttpContext.SetOverriddenBrowser(
                mobile ? BrowserOverride.Mobile : BrowserOverride.Desktop);
            return Redirect(returnUrl);
        }

    }
}

