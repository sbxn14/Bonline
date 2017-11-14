using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bonline.Models;

namespace Bonline.Database
{
 public static class Datamanager
 {
  public static List<Account> AccList;
  public static List<Bon> BonList;
  public static List<Locatie> LocList;
  public static List<Organisatie> OrgList;

  public static void Initialize()
  {
   AccList = DB.RunQuery(new Account());
   OrgList = DB.RunQuery(new Organisatie());
   LocList = DB.RunQuery(new Locatie());
   BonList = DB.RunQuery(new Bon());
  }
 }
}