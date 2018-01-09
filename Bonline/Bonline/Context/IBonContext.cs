using System.Collections.Generic;
using Bonline.Models;

namespace Bonline.Context
{
    public interface IBonContext
    {
        void Insert(Bon b);
        List<Bon> Select();
        void InsertKassa(Bon b);
        Bon GetOrgName(Bon b);
        int GetLocId(Bon b);
        void AddLocId(Bon b);
        ImageModel GetImage(int imageId);
    }
}
