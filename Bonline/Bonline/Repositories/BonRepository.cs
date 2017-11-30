<<<<<<< refs/remotes/origin/Kassasysteem
<<<<<<< refs/remotes/origin/Kassasysteem
﻿using System;
=======
﻿using System.Linq;
>>>>>>> Details bonnen werkt, Zoeken niet meer?
=======
﻿using System.Collections.Generic;
using System.Linq;
>>>>>>> filter met organisatie
using Bonline.Context;
using Bonline.Models;

namespace Bonline.Repositories
{
 public class BonRepository
 {
<<<<<<< refs/remotes/origin/Kassasysteem
        IBonContext context;

        public BonRepository(IBonContext context)
        {
            this.context = context;
        }

        public void InsertKassa(Bon b)
        {
            this.context.InsertKassa(b);
        }

        public Bon GetOrgName(Bon b)
        {
            return context.GetOrgName(b);
        }

        public void GetLocName(Bon b)
        {
            this.context.GetLocName(b);
        }



    
=======
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

<<<<<<< refs/remotes/origin/Kassasysteem
>>>>>>> Details bonnen werkt, Zoeken niet meer?
=======
  public IEnumerable<Bon> SelectBonnen(int userId)
  {
   IEnumerable<Bon> bon = (from b in this._context.Select()
					  where b.AccId.Equals(userId)
					  select b);
   return bon;
  }

<<<<<<< refs/remotes/origin/Kassasysteem
>>>>>>> filter met organisatie

=======
  public IEnumerable<Bon> SelectBonnenOrg(string org)
  {
   IEnumerable<Bon> bon = (from b in this._context.Select()
					  where b.LocatieId.Equals()
					  select b);
   return bon;
  }
>>>>>>> filter werkt bijna
 }
}