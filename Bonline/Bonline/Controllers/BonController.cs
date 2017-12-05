using System;
using System.Collections.Generic;
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
			HttpCookie c = Request.Cookies[".ASPXAUTH"];
			TicketAuth auth = new TicketAuth();
			Datamanager.Initialize();
			return View((List<Bon>)_bonRepository.SelectBonnen(auth.Decrypt()));
		}


		[HttpPost]
		public ActionResult BonKassa(Bon b) => View("Kassa");

		[HttpPost]
		public ActionResult Bon(Bon bon, string orgnaam = "0")
		{
			if (orgnaam == "0")
			{
				return RedirectToAction("Details", "Bon", bon);
			}
			List<Bon> bonnen = Datamanager.BonList;
			return View(bonnen);
		}

		//[HttpPost]
		//public ActionResult Bondirect(Bon b)
		//{
		// DateTime date = DateTime.Now;
		// string description = "lijst hier, " + date;
		// int locatieId = 5;
		// Bon f = new Bon(b.AccId, date, description, locatieId);
		// _bonRepository.InsertKassa(f);

		// //repo.GetOrgName(f).Locatie, repo.GetOrgName(f).Org
		// string locatie = _bonRepository.GetOrgName(f).Locatie;
		// string organisatie = _bonRepository.GetOrgName(f).Org;

		// Bon g = new Bon(b.AccId, date, description, locatie, organisatie);
		// return View("BonOverzicht", g);
		//}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Details(Bon bon)
		{
			return View("Details", bon);
		}

		public ActionResult GoToKassa()
		{
			return View("Kassa");
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
