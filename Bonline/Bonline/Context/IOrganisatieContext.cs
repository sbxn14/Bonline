using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonline.Models;

namespace Bonline.Context
{
 public interface IOrganisatieContext
 {
  void Insert(Organisatie org);
  List<Organisatie> Select();
  void Update(int id, string NieuwNaam);
  void Delete(int id);
 }
}
