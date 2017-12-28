using System.Linq;
using System.Collections.Generic;
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

        public void InsertKassa(Bon b)
        {
            _context.InsertKassa(b);
        }

        public Bon SelectBon(int id)
        {
            Bon bon = (from b in _context.Select()
                       where b.Id.Equals(id)
                       select b).Single();
            return bon;
        }

        public IEnumerable<Bon> SelectBonnen(int userId)
        {
            IEnumerable<Bon> bon = (from b in _context.Select()
                                    where b.AccId.Equals(userId)
                                    select b);
            return bon;
        }

        //public IEnumerable<Bon> SelectBonnenOrg(string org)
        //{
        // IEnumerable<Bon> bon = (from b in this._context.Select()
        //			  where b.LocatieId.Equals()
        //			  select b);
        // return bon;
        //}

        public List<string> GetAllOrgs()
        {
            List<string> orgs = (from b in _context.Select()
                                 select b.Loc.Org.Name).ToList();
            orgs = orgs.Distinct().ToList();
            return orgs;
        }

        public void AddBon(Bon bon)
        {
            _context.Insert(bon);
        }

        public List<Bon> GetBonnenMetOrgNaam(string orgnaam, List<Bon> bonnenList)
        {
            List<Bon> bonnen = bonnenList.Where(x => x.Loc.Org.Name.Equals(orgnaam)).ToList();
            return bonnen;
        }

    }
}