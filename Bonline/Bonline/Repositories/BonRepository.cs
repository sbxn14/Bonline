<<<<<<< refs/remotes/origin/Kassasysteem
﻿using System;
=======
﻿using System.Linq;
>>>>>>> Details bonnen werkt, Zoeken niet meer?
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

>>>>>>> Details bonnen werkt, Zoeken niet meer?

 }
}