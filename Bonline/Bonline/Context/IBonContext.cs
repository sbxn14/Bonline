﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bonline.Models;

namespace Bonline.Context
{
 public interface IBonContext
 {
	 void Insert(Bon b);
	 List<Bon> Select();

     void InsertKassa(Bon b);

        Bon GetOrgName(Bon b);

        void GetLocName(Bon b);

   

    
    
 }
}
