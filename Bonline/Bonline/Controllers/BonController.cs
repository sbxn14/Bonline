using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonline.Context.MSSQL;
using Bonline.Database;
using Bonline.Models;
using Bonline.Repositories;

namespace Bonline.Controllers
{
 public class BonController : Controller
 {
  private BonRepository bonRepository = new BonRepository(new MssqlBonContext());


  [HttpGet]
  public ActionResult Bon()
  {
   //Datamanager.Initialize();

   // List<Bon> BonnenList = Datamanager.BonList;
   List<Bon> bonnenList = DB.RunQuery(new Bon());
   var model = bonnenList;
   return View(model);
  }

  [HttpPost]
  public ActionResult Bon(Bon bon)
  {
   return RedirectToAction("Details", "Bon", bon);
  }



  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Details(Bon bon)
  {
   return View("Details", bon);
  }

  //TODO verbeteren, zodat er geen exception nodig is.
  [HttpGet]
  public ActionResult Details(int id = 0)
  {
   try
   {
    Bon bon = bonRepository.SelectBon(id);
    return Details(bon);
   }
   catch (Exception e)
   {
    Console.WriteLine(e);
    throw;
   }
   return View();
  }
 }
}