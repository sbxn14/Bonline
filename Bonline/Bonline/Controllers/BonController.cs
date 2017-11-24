using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonline.Database;
using Bonline.Models;
using Bonline.Repositories;

namespace Bonline.Controllers
{
 public class BonController : Controller
 {
  [HttpGet]
  public ActionResult Bon()
  {
   //Datamanager.Initialize();

   // List<Bon> BonnenList = Datamanager.BonList;
   List<Bon> BonnenList = DB.RunQuery(new Bon());
   var model = BonnenList;
   return View(model);
  }

  [HttpPost]
  public ActionResult Bon(Bon bon)
  {
   return RedirectToAction("Details", "Bon", bon);
  }



  [HttpGet]
  public ActionResult Details()
  {
   return View("Details","Bon");
  }

  [HttpPost]
  public ActionResult Details(Bon bon)
  {
   return View("Details", "Bon");
  }
 }
}