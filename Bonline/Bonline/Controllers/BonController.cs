using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonline.Database;
using Bonline.Models;
using Bonline.Repositories;
<<<<<<< refs/remotes/origin/Kassasysteem
using Bonline.Context.MSSQL;

=======
>>>>>>> Accounts view

namespace Bonline.Controllers
{
 public class BonController : Controller
 {
        BonRepository repo = new BonRepository(new MssqlBonContext());


  [HttpGet]
  public ActionResult Bon()
  {
   //Datamanager.Initialize();

   // List<Bon> BonnenList = Datamanager.BonList;
   List<Bon> bonnenList = DB.RunQuery(new Bon());
   var model = bonnenList;
   return View(model);
  }

<<<<<<< refs/remotes/origin/Kassasysteem
        [HttpPost]
        public ActionResult BonKassa(Bon b) => View("Kassa");
=======
  [HttpPost]
  public ActionResult Bon(Bon bon)
  {
   return RedirectToAction("Details", "Bon", bon);
  }

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


        public ActionResult GoToKassa()
        {
            return View("Kassa");
            
        }


    }


 
=======
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
>>>>>>> Accounts view
}