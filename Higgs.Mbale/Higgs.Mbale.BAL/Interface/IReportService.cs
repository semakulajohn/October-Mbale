using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface IReportService
 {
     #region transactions
     IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate);

        IEnumerable<Transaction> GenerateTransactionCurrentMonthReport();

        IEnumerable<Transaction> GenerateTransactionTodaysReport();

        IEnumerable<Transaction> GenerateTransactionCurrentWeekReport();

#endregion

     #region supplies
         IEnumerable<Supply> GenerateSupplyCurrentMonthReport();
       
         IEnumerable<Supply> GenerateSupplyCurrentWeekReport();
       
         IEnumerable<Supply> GenerateSupplyTodaysReport();

         IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate,long branchId,string supplierId);
       
     #endregion
     
     #region supplies for supplier
         IEnumerable<Supply> GenerateSupplyCurrentMonthReportForAParticularSupplier(string supplierId);

         IEnumerable<Supply> GenerateSupplyCurrentWeekReportForAParticularSupplier(string supplierId);

         IEnumerable<Supply> GenerateSupplyTodaysReportForAParticularSupplier(string supplierId);

         IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDatesForAParticularSupplier(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate,string supplierId);

         #endregion

         #region accountTransactions
          IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentMonthReport();
        
          IEnumerable<AccountTransactionActivity> GenerateAccountTransactionTodaysReport();
        
          IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentWeekReport();

          IEnumerable<AccountTransactionActivity> GetAllAccountTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string supplierId);
       
         #endregion

          #region batches

          IEnumerable<Batch> GenerateBatchCurrentMonthReport();

          IEnumerable<Batch> GenerateBatchCurrentWeekReport();
         
          IEnumerable<Batch> GenerateBatchTodaysReport();

          IEnumerable<Batch> GetAllBatchesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId);
         
          #endregion

          #region deliveries
          IEnumerable<Delivery> GenerateDeliveryCurrentMonthReport();
         
           IEnumerable<Delivery> GenerateDeliveryCurrentWeekReport();
          
          IEnumerable<Delivery> GenerateDeliveryTodaysReport();

          IEnumerable<Delivery> GetAllDeliveriesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string customerId);
         
          #endregion

          #region cash

          IEnumerable<Cash> GenerateCashCurrentMonthReport();

          IEnumerable<Cash> GenerateCashCurrentWeekReport();

          IEnumerable<Cash> GenerateCashTodaysReport();

          IEnumerable<Cash> GetAllCashBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId);

          #endregion

          #region orders
          IEnumerable<Order> GenerateOrderCurrentMonthReport();

          IEnumerable<Order> GenerateOrderCurrentWeekReport();

          IEnumerable<Order> GenerateOrderTodaysReport();

          IEnumerable<Order> GetAllOrdersBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string customerId);

          #endregion


 }
}
