using System.Collections.Generic;
using System.Linq;
using Bonline.Context;
using Bonline.Models;

namespace Bonline.Repositories
{
 public class BonRepository
 {
  readonly IBonContext _context;

  public BonRepository(IBonContext context)
  {
   _context = context;
  }

  public Bon SelectBon(int id)
  {
   Bon bon = (from b in this._context.Select()
		    where b.Id.Equals(id)
		    select b).Single();
   return bon;
  }

  public IEnumerable<Bon> SelectBonnen(int userId)
  {
   IEnumerable<Bon> bon = (from b in this._context.Select()
					  where b.AccId.Equals(userId)
					  select b);
   return bon;
  }

  public IEnumerable<Bon> SelectBonnenOrg(string org)
  {
   IEnumerable<Bon> bon = (from b in this._context.Select()
					  where b.LocatieId.Equals()
					  select b);
   return bon;
  }
 }
}