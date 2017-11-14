using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonline.Database;
using Bonline.Models;

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
  public ActionResult Bon(FormCollection form)
  {


   return View();
  }
 }
}