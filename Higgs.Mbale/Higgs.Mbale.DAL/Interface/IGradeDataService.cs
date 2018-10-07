﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface IGradeDataService
    {
        IEnumerable<Grade> GetAllGrades();
        IEnumerable<Size> GetAllSizes();
        Size GetSize(long sizeId);
    }
}
