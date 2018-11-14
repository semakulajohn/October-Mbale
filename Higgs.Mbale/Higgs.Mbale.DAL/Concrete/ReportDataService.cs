using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.DAL.Concrete;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.EF.UnitOfWork;
using Higgs.Mbale.DTO;
using log4net;

namespace Higgs.Mbale.DAL.Concrete
{
  public  class ReportDataService : DataServiceBase, IReportDataService
    {
          ILog logger = log4net.LogManager.GetLogger(typeof(ReportDataService));

       public ReportDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
       #region Transactions
       public IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate)
       {
           return this.UnitOfWork.Get<Transaction>().AsQueryable()
               .Where(m => m.Deleted ==false &&(m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<Transaction> GenerateTransactionCurrentMonthReport()
       {
           return this.UnitOfWork.Get<Transaction>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Transaction> GenerateTransactionTodaysReport()
       {
           return this.UnitOfWork.Get<Transaction>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Transaction> GenerateTransactionCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Transaction>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }


       #endregion

       #region accountTransactions
       public IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentMonthReport()
       {
           return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<AccountTransactionActivity> GenerateAccountTransactionTodaysReport()
       {
           return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       public IEnumerable<AccountTransactionActivity> GetAllAccountTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string supplierId)
       {
           if (branchId != 0 && supplierId == null)
           {

               return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }
           else if (supplierId != null && branchId == 0)
           {

               return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.AspNetUserId == supplierId);
           }
           else if (supplierId != null && branchId != 0)
           {
               return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.AspNetUserId == supplierId && m.BranchId == branchId);
           }
           return this.UnitOfWork.Get<AccountTransactionActivity>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       #endregion

       #region Supplies
       public IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate,long branchId,string supplierId)
       {
           if (branchId != 0 && supplierId == null)
           {

               return this.UnitOfWork.Get<Supply>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.SupplyDate >= lowerSpecifiedDate && m.SupplyDate <= upperSpecifiedDate) && m.BranchId == branchId);
           }
           else if (supplierId != null && branchId == 0)
           {

               return this.UnitOfWork.Get<Supply>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.SupplyDate >= lowerSpecifiedDate && m.SupplyDate <= upperSpecifiedDate)&& m.SupplierId == supplierId);
           }
           else if (supplierId != null && branchId != 0)
           {
               return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(m => m.Deleted == false && (m.SupplyDate >= lowerSpecifiedDate && m.SupplyDate <= upperSpecifiedDate) && m.SupplierId == supplierId && m.BranchId == branchId);
           }
           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(m => m.Deleted == false && (m.SupplyDate >= lowerSpecifiedDate && m.SupplyDate <= upperSpecifiedDate));
       }

       public IEnumerable<Supply> GenerateSupplyCurrentMonthReport()
       {
           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Supply> GenerateSupplyTodaysReport()
       {
           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Supply> GenerateSupplyCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       #endregion 

        #region supplies for particular supplier and branch
       public IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDatesForAParticularSupplier(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate,string supplierId)
       {
           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate)&& m.SupplierId == supplierId);
       }

       public IEnumerable<Supply> GenerateSupplyCurrentMonthReportForAParticularSupplier(string supplierId)
       {
           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year && p.SupplierId == supplierId);
       }

       public IEnumerable<Supply> GenerateSupplyTodaysReportForAParticularSupplier(string supplierId)
       {
           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year && p.SupplierId == supplierId);
       }

       public IEnumerable<Supply> GenerateSupplyCurrentWeekReportForAParticularSupplier(string supplierId)
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Supply>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate && p.SupplierId == supplierId);
       }
        #endregion

        #region  batches
       public IEnumerable<Batch> GetAllBatchesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
       {
           if (branchId != 0 )
           {

               return this.UnitOfWork.Get<Batch>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }
          
          
           return this.UnitOfWork.Get<Batch>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<Batch> GenerateBatchCurrentMonthReport()
       {
           return this.UnitOfWork.Get<Batch>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Batch> GenerateBatchTodaysReport()
       {
           return this.UnitOfWork.Get<Batch>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Batch> GenerateBatchCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Batch>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

        #endregion

       #region  Factory Expenses
       public IEnumerable<FactoryExpense> GetAllFactoryExpensesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
       {
           if (branchId != 0)
           {

               return this.UnitOfWork.Get<FactoryExpense>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }


           return this.UnitOfWork.Get<FactoryExpense>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<FactoryExpense> GenerateFactoryExpenseCurrentMonthReport()
       {
           return this.UnitOfWork.Get<FactoryExpense>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<FactoryExpense> GenerateFactoryExpenseTodaysReport()
       {
           return this.UnitOfWork.Get<FactoryExpense>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<FactoryExpense> GenerateFactoryExpenseCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<FactoryExpense>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       #endregion

       #region  Other Expenses
       public IEnumerable<OtherExpense> GetAllOtherExpensesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
       {
           if (branchId != 0)
           {

               return this.UnitOfWork.Get<OtherExpense>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }


           return this.UnitOfWork.Get<OtherExpense>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<OtherExpense> GenerateOtherExpenseCurrentMonthReport()
       {
           return this.UnitOfWork.Get<OtherExpense>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<OtherExpense> GenerateOtherExpenseTodaysReport()
       {
           return this.UnitOfWork.Get<OtherExpense>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<OtherExpense> GenerateOtherExpenseCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<OtherExpense>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       #endregion

       #region  batchoutputs
       public IEnumerable<BatchOutPut> GetAllBatchOutPutsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
       {
           if (branchId != 0)
           {

               return this.UnitOfWork.Get<BatchOutPut>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }


           return this.UnitOfWork.Get<BatchOutPut>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<BatchOutPut> GenerateBatchOutPutCurrentMonthReport()
       {
           return this.UnitOfWork.Get<BatchOutPut>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<BatchOutPut> GenerateBatchOutPutTodaysReport()
       {
           return this.UnitOfWork.Get<BatchOutPut>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<BatchOutPut> GenerateBatchOutPutCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<BatchOutPut>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       #endregion

       #region  LabourCosts
       public IEnumerable<LabourCost> GetAllLabourCostsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
       {
           if (branchId != 0)
           {

               return this.UnitOfWork.Get<LabourCost>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }


           return this.UnitOfWork.Get<LabourCost>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<LabourCost> GenerateLabourCostCurrentMonthReport()
       {
           return this.UnitOfWork.Get<LabourCost>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<LabourCost> GenerateLabourCostTodaysReport()
       {
           return this.UnitOfWork.Get<LabourCost>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<LabourCost> GenerateLabourCostCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<LabourCost>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       #endregion

       #region  MachineRepair
       public IEnumerable<MachineRepair> GetAllMachineRepairsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
       {
           if (branchId != 0)
           {

               return this.UnitOfWork.Get<MachineRepair>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }


           return this.UnitOfWork.Get<MachineRepair>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<MachineRepair> GenerateMachineRepairCurrentMonthReport()
       {
           return this.UnitOfWork.Get<MachineRepair>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<MachineRepair> GenerateMachineRepairTodaysReport()
       {
           return this.UnitOfWork.Get<MachineRepair>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<MachineRepair> GenerateMachineRepairCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<MachineRepair>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       #endregion

       #region  Utility
       public IEnumerable<Utility> GetAllUtilitiesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
       {
           if (branchId != 0)
           {

               return this.UnitOfWork.Get<Utility>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }


           return this.UnitOfWork.Get<Utility>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<Utility> GenerateUtilityCurrentMonthReport()
       {
           return this.UnitOfWork.Get<Utility>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Utility> GenerateUtilityTodaysReport()
       {
           return this.UnitOfWork.Get<Utility>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Utility> GenerateUtilityCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Utility>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       #endregion


       #region  FlourTransfer
       public IEnumerable<FlourTransfer> GetAllFlourTransfersBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
       {
           if (branchId != 0)
           {

               return this.UnitOfWork.Get<FlourTransfer>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }


           return this.UnitOfWork.Get<FlourTransfer>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<FlourTransfer> GenerateFlourTransferCurrentMonthReport()
       {
           return this.UnitOfWork.Get<FlourTransfer>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<FlourTransfer> GenerateFlourTransferTodaysReport()
       {
           return this.UnitOfWork.Get<FlourTransfer>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<FlourTransfer> GenerateFlourTransferCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<FlourTransfer>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       #endregion

       #region Deliveries
       public IEnumerable<Delivery> GetAllDeliveriesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string customerId)
       {
           if (branchId != 0 && customerId == null)
           {

               return this.UnitOfWork.Get<Delivery>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }
           else if (customerId != null && branchId == 0)
           {

               return this.UnitOfWork.Get<Delivery>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.CustomerId == customerId);
           }
           else if (customerId != null && branchId != 0)
           {
               return this.UnitOfWork.Get<Delivery>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.CustomerId == customerId && m.BranchId == branchId);
           }
           return this.UnitOfWork.Get<Delivery>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<Delivery> GenerateDeliveryCurrentMonthReport()
       {
           return this.UnitOfWork.Get<Delivery>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Delivery> GenerateDeliveryTodaysReport()
       {
           return this.UnitOfWork.Get<Delivery>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Delivery> GenerateDeliveryCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Delivery>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }

       #endregion 

        #region cash

       public IEnumerable<Cash> GetAllCashBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
       {
           if (branchId != 0)
           {

               return this.UnitOfWork.Get<Cash>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }


           return this.UnitOfWork.Get<Cash>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<Cash> GenerateCashCurrentMonthReport()
       {
           return this.UnitOfWork.Get<Cash>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Cash> GenerateCashTodaysReport()
       {
           return this.UnitOfWork.Get<Cash>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Cash> GenerateCashCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Cash>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }
        #endregion

        #region orders

       public IEnumerable<Order> GetAllOrdersBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string customerId)
       {
           if (branchId != 0 && customerId == null)
           {

               return this.UnitOfWork.Get<Order>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.BranchId == branchId);
           }
           else if (customerId != null && branchId == 0)
           {

               return this.UnitOfWork.Get<Order>().AsQueryable()
                   .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.CustomerId == customerId);
           }
           else if (customerId != null && branchId != 0)
           {
               return this.UnitOfWork.Get<Order>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate) && m.CustomerId == customerId && m.BranchId == branchId);
           }
           return this.UnitOfWork.Get<Order>().AsQueryable()
               .Where(m => m.Deleted == false && (m.CreatedOn >= lowerSpecifiedDate && m.CreatedOn <= upperSpecifiedDate));
       }

       public IEnumerable<Order> GenerateOrderCurrentMonthReport()
       {
           return this.UnitOfWork.Get<Order>().AsQueryable()
               .Where(p => p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Order> GenerateOrderTodaysReport()
       {
           return this.UnitOfWork.Get<Order>().AsQueryable()
               .Where(p => p.CreatedOn.Day == DateTime.Now.Day && p.CreatedOn.Month == DateTime.Now.Month && p.CreatedOn.Year == DateTime.Now.Year);
       }

       public IEnumerable<Order> GenerateOrderCurrentWeekReport()
       {

           DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1);
           DateTime endDate = DateTime.Now;

           return this.UnitOfWork.Get<Order>().AsQueryable()
               .Where(p => p.CreatedOn >= startOfWeek && p.CreatedOn <= endDate);
       }
        #endregion
    }
}
