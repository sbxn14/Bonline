﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonline.Context.MSSQL;
using Bonline.Database;
using Bonline.Models;
using Bonline.Repositories;
<<<<<<< HEAD
<<<<<<< refs/remotes/origin/Kassasysteem
using Bonline.Context.MSSQL;

=======
>>>>>>> Accounts view
=======
>>>>>>> master

namespace Bonline.Controllers
{
 public class BonController : Controller
 {
<<<<<<< HEAD
<<<<<<< refs/remotes/origin/Kassasysteem
<<<<<<< refs/remotes/origin/Kassasysteem
        BonRepository repo = new BonRepository(new MssqlBonContext());
=======
  private BonRepository bonRepository = new BonRepository(new MssqlBonContext());
>>>>>>> Details bonnen werkt, Zoeken niet meer?

=======
  private readonly BonRepository _bonRepository = new BonRepository(new MssqlBonContext());
<<<<<<< refs/remotes/origin/Kassasysteem
>>>>>>> filter met organisatie

=======
>>>>>>> filler stuff
=======
  private readonly BonRepository _bonRepository = new BonRepository(new MssqlBonContext());
>>>>>>> master
  [HttpGet]
  public ActionResult Bon(HttpCookie c)
  {
   TicketAuth auth = new TicketAuth();
   Datamanager.Initialize();
   return View((List<Bon>)_bonRepository.SelectBonnen(auth.Decrypt()));
  }

<<<<<<< refs/remotes/origin/Kassasysteem
        [HttpPost]
        public ActionResult BonKassa(Bon b) => View("Kassa");
=======
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
<<<<<<< HEAD

<<<<<<< refs/remotes/origin/Kassasysteem
>>>>>>> Accounts view

        [HttpPost]
        public ActionResult Bondirect(Bon b)
        {

<<<<<<< refs/remotes/origin/Kassasysteem
            

            DateTime Date = DateTime.Now;
            string Description = "lijst hier, " + Date.ToString();
            int LocatieId = 5;
            Bon f = new Bon(b.AccId, Date, Description, LocatieId);
            repo.InsertKassa(f);

            //repo.GetOrgName(f).Locatie, repo.GetOrgName(f).Org
            string Locatie = repo.GetOrgName(f).Locatie;
            string organisatie = repo.GetOrgName(f).Org;

            Bon G = new Bon(b.AccId, Date, Description, Locatie, organisatie);       
            
            
            return View("BonOverzicht", G);
        }
=======
>>>>>>> master

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Details(Bon bon)
  {
   return View("Details", bon);
  }

<<<<<<< HEAD
        public ActionResult GoToKassa()
        {
            return View("Kassa");
            
        }


    }


<<<<<<< refs/remotes/origin/Kassasysteem
 
=======
  [HttpGet]
  public ActionResult Details()
=======
=======
>>>>>>> filter met organisatie
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Details(Bon bon)
>>>>>>> Details bonnen werkt, Zoeken niet meer?
  {
   return View("Details", bon);
  }

=======
>>>>>>> master
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
>>>>>>> Accounts view
}