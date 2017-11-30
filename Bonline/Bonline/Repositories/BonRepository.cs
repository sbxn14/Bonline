using System;
using Bonline.Context;
using Bonline.Models;

namespace Bonline.Repositories
{
 public class BonRepository
 {
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



    

 }
}