using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonline.Controllers
{
    public class BonController : Controller
    {
        // GET: Bon
        public ActionResult Index()
        {
            return View();
        }
    }
}