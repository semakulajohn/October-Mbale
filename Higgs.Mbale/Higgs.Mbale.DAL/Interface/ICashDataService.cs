using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface ICashDataService
    {
        Cash GetCash(long cashId);
        IEnumerable<Cash> GetAllCash();

        IEnumerable<Cash> GetAllCashForAParticularBranch(long branchId);

        long SaveCash(CashDTO cashDTO, string userId);

        void MarkAsDeleted(long cashId, string userId);
        Cash GetLatestCashForAParticularBranch(long branchId);

        Application GetApplicationDetails();
        long UpdateApplicationCash(ApplicationDTO applicationDTO);
        
        
    }
}
