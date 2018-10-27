using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface ICashService
    {
        Cash GetCash(long cashId);
        IEnumerable<Cash> GetAllCash();

        IEnumerable<Cash> GetAllCashForAParticularBranch(long branchId);

        long SaveCash(Cash cash, string userId);

        void MarkAsDeleted(long cashId, string userId);
        void SaveApplicationCash(Cash cash,string userId);

       // Cash GetLatestCashForAParticularBranch(long branchId);
    }
}
