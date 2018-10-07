using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface IReportDataService
  {
      #region transactions
      IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate);

         IEnumerable<Transaction> GenerateTransactionCurrentMonthReport();

         IEnumerable<Transaction> GenerateTransactionTodaysReport();

         IEnumerable<Transaction> GenerateTransactionCurrentWeekReport();
              #endregion

         #region supplies
         IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate,long branchId,string supplierId);
        
         IEnumerable<Supply> GenerateSupplyCurrentMonthReport();
        
          IEnumerable<Supply> GenerateSupplyTodaysReport();

          IEnumerable<Supply> GenerateSupplyCurrentWeekReport();
         
#endregion
      #region supplier for particular supplier

          IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDatesForAParticularSupplier(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate,string supplierId);

          IEnumerable<Supply> GenerateSupplyCurrentMonthReportForAParticularSupplier(string supplierId);

          IEnumerable<Supply> GenerateSupplyTodaysReportForAParticularSupplier(string supplierId);

          IEnumerable<Supply> GenerateSupplyCurrentWeekReportForAParticularSupplier(string supplierId);
      #endregion

      #region accountTransactions
          
           IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentMonthReport();
         
           IEnumerable<AccountTransactionActivity> GenerateAccountTransactionTodaysReport();
         
          IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentWeekReport();

          IEnumerable<AccountTransactionActivity> GetAllAccountTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string supplierId);
        

      #endregion
  }
}
