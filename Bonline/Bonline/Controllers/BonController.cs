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
  private readonly BonRepository _bonRepository = new BonRepository(new MssqlBonContext());

  [HttpGet]
  public ActionResult Bon()
  {
   Datamanager.Initialize();
   return View(Datamanager.BonList);
  }

  [HttpPost]
  public ActionResult Bon(Bon bon, string orgnaam = "0")
  {
   if (orgnaam == "0")
   {
    return RedirectToAction("Details", "Bon", bon);
   }
   else
   {
    List<Bon> bonnen = Datamanager.BonList;

    return View(bon);
   }
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
    Bon bon = _bonRepository.SelectBon(id);
    return Details(bon);
   }
   catch (Exception e)
   {
    Console.WriteLine(e);
    throw;
   }
  }
 }
}