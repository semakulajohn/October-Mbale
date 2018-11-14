using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Higgs.Mbale.BAL.Interface;
using log4net;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.Web.Controllers
{
    public class ReportApiController : ApiController
    {
          private IReportService _reportService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(ReportApiController));
            private string userId = string.Empty;

            public ReportApiController()
            {
            }

            public ReportApiController(IReportService reportService,IUserService userService)
            {
                this._reportService = reportService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }


            #region transactions
            [HttpGet]
            [ActionName("GetAllTransactionsBetweenTheSpecifiedDates")]
            public IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(ReportSearch search)
            {
                return _reportService.GetAllTransactionsBetweenTheSpecifiedDates(search.FromDate,search.ToDate);
            }

            [HttpGet]
            [ActionName("GenerateTransactionCurrentMonthReport")]
            public IEnumerable<Transaction> GenerateTransactionCurrentMonthReport()
            {
                return _reportService.GenerateTransactionCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateTransactionTodaysReport")]
            public IEnumerable<Transaction> GenerateTransactionTodaysReport()
            {
                return _reportService.GenerateTransactionTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateTransactionCurrentWeekReport")]
            public IEnumerable<Transaction> GenerateTransactionCurrentWeekReport()
            {
                return _reportService.GenerateTransactionCurrentWeekReport();
            }

           
      
            #endregion

        #region supplies
            [HttpPost]
            [ActionName("GetAllSuppliesBetweenTheSpecifiedDates")]
            public IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllSuppliesBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate,searchDates.BranchId,searchDates.SupplierId);
            }

            [HttpGet]
            [ActionName("GenerateSupplyCurrentMonthReport")]
            public IEnumerable<Supply> GenerateSupplyCurrentMonthReport()
            {
                return _reportService.GenerateSupplyCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateSupplyTodaysReport")]
            public IEnumerable<Supply> GenerateSupplyTodaysReport()
            {
                return _reportService.GenerateSupplyTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateSupplyCurrentWeekReport")]
            public IEnumerable<Supply> GenerateSupplyCurrentWeekReport()
            {
                return _reportService.GenerateSupplyCurrentWeekReport();
            }
        #endregion

            #region supplies for supplier
            [HttpPost]
            [ActionName("GetAllSuppliesBetweenTheSpecifiedDatesForAParticularSupplier")]
            public IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDatesForAParticularSupplier(ReportSearch searchDates,string supplierId)
            {
                return _reportService.GetAllSuppliesBetweenTheSpecifiedDatesForAParticularSupplier(searchDates.FromDate, searchDates.ToDate,supplierId);
            }

            [HttpGet]
            [ActionName("GenerateSupplyCurrentMonthReportForAParticularSupplier")]
            public IEnumerable<Supply> GenerateSupplyCurrentMonthReportforAParticularSupplier(string supplierId)
            {
                return _reportService.GenerateSupplyCurrentMonthReportForAParticularSupplier(supplierId);
            }

            [HttpGet]
            [ActionName("GenerateSupplyTodaysReportForAParticularSupplier")]
            public IEnumerable<Supply> GenerateSupplyTodaysReportForAParticularSupplier(string supplierId)
            {
                return _reportService.GenerateSupplyTodaysReportForAParticularSupplier(supplierId);
            }

            [HttpGet]
            [ActionName("GenerateSupplyCurrentWeekReportForAParticularSupplier")]
            public IEnumerable<Supply> GenerateSupplyCurrentWeekReportForAParticularSupplier(string supplierId)
            {
                return _reportService.GenerateSupplyCurrentWeekReportForAParticularSupplier(supplierId);
            }
            #endregion

        #region accountTransactions

           

            [HttpGet]
            [ActionName("GenerateAccountTransactionCurrentMonthReport")]
            public IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentMonthReport()
            {
                return _reportService.GenerateAccountTransactionCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateAccountTransactionTodaysReport")]
            public IEnumerable<AccountTransactionActivity> GenerateAccountTransactionTodaysReport()
            {
                return _reportService.GenerateAccountTransactionTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateAccountTransactionCurrentWeekReport")]
            public IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentWeekReport()
            {
                return _reportService.GenerateAccountTransactionCurrentWeekReport();
            }

            [HttpPost]
            [ActionName("GenerateAccountTransactionsBetweenTheSpecifiedDates")]
            public IEnumerable<AccountTransactionActivity> GetAllAccountTransactionsBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllAccountTransactionsBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId, searchDates.SupplierId);

            }
        #endregion

         #region batches
            [HttpPost]
            [ActionName("GetAllBatchesBetweenTheSpecifiedDates")]
            public IEnumerable<Batch> GetAllBatchesBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllBatchesBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId);
            }

            [HttpGet]
            [ActionName("GenerateBatchCurrentMonthReport")]
            public IEnumerable<Batch> GenerateBatchCurrentMonthReport()
            {
                return _reportService.GenerateBatchCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateBatchTodaysReport")]
            public IEnumerable<Batch> GenerateBatchTodaysReport()
            {
                return _reportService.GenerateBatchTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateBatchCurrentWeekReport")]
            public IEnumerable<Batch> GenerateBatchCurrentWeekReport()
            {
                return _reportService.GenerateBatchCurrentWeekReport();
            }
            #endregion

         #region deliveries
            [HttpPost]
            [ActionName("GetAllDeliveriesBetweenTheSpecifiedDates")]
            public IEnumerable<Delivery> GetAllDeliveriesBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllDeliveriesBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId, searchDates.CustomerId);
            }

            [HttpGet]
            [ActionName("GenerateDeliveryCurrentMonthReport")]
            public IEnumerable<Delivery> GenerateDeliveryCurrentMonthReport()
            {
                return _reportService.GenerateDeliveryCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateDeliveryTodaysReport")]
            public IEnumerable<Delivery> GenerateDeliveryTodaysReport()
            {
                return _reportService.GenerateDeliveryTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateDeliveryCurrentWeekReport")]
            public IEnumerable<Delivery> GenerateDeliveryCurrentWeekReport()
            {
                return _reportService.GenerateDeliveryCurrentWeekReport();
            }
            #endregion

            #region FactoryExpenses
            [HttpPost]
            [ActionName("GetAllFactoryExpensesBetweenTheSpecifiedDates")]
            public IEnumerable<FactoryExpense> GetAllFactoryExpensesBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllFactoryExpensesBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId);
            }

            [HttpGet]
            [ActionName("GenerateFactoryExpenseCurrentMonthReport")]
            public IEnumerable<FactoryExpense> GenerateFactoryExpenseCurrentMonthReport()
            {
                return _reportService.GenerateFactoryExpenseCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateFactoryExpenseTodaysReport")]
            public IEnumerable<FactoryExpense> GenerateFactoryExpenseTodaysReport()
            {
                return _reportService.GenerateFactoryExpenseTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateFactoryExpenseCurrentWeekReport")]
            public IEnumerable<FactoryExpense> GenerateFactoryExpenseCurrentWeekReport()
            {
                return _reportService.GenerateFactoryExpenseCurrentWeekReport();
            }
            #endregion

            #region orders
            [HttpPost]
            [ActionName("GetAllOrdersBetweenTheSpecifiedDates")]
            public IEnumerable<Order> GetAllOrdersBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllOrdersBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId, searchDates.CustomerId);
            }

            [HttpGet]
            [ActionName("GenerateOrderCurrentMonthReport")]
            public IEnumerable<Order> GenerateOrderCurrentMonthReport()
            {
                return _reportService.GenerateOrderCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateOrderTodaysReport")]
            public IEnumerable<Order> GenerateOrderTodaysReport()
            {
                return _reportService.GenerateOrderTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateOrderCurrentWeekReport")]
            public IEnumerable<Order> GenerateOrderCurrentWeekReport()
            {
                return _reportService.GenerateOrderCurrentWeekReport();
            }
            #endregion

            #region Cash
            [HttpPost]
            [ActionName("GetAllCashBetweenTheSpecifiedDates")]
            public IEnumerable<Cash> GetAllCashBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllCashBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId);
            }

            [HttpGet]
            [ActionName("GenerateCashCurrentMonthReport")]
            public IEnumerable<Cash> GenerateCashCurrentMonthReport()
            {
                return _reportService.GenerateCashCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateCashTodaysReport")]
            public IEnumerable<Cash> GenerateCashTodaysReport()
            {
                return _reportService.GenerateCashTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateCashCurrentWeekReport")]
            public IEnumerable<Cash> GenerateCashCurrentWeekReport()
            {
                return _reportService.GenerateCashCurrentWeekReport();
            }
            #endregion

            #region OtherExpenses
            [HttpPost]
            [ActionName("GetAllOtherExpensesBetweenTheSpecifiedDates")]
            public IEnumerable<OtherExpense> GetAllOtherExpensesBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllOtherExpensesBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId);
            }

            [HttpGet]
            [ActionName("GenerateOtherExpenseCurrentMonthReport")]
            public IEnumerable<OtherExpense> GenerateOtherExpenseCurrentMonthReport()
            {
                return _reportService.GenerateOtherExpenseCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateOtherExpenseTodaysReport")]
            public IEnumerable<OtherExpense> GenerateOtherExpenseTodaysReport()
            {
                return _reportService.GenerateOtherExpenseTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateOtherExpenseCurrentWeekReport")]
            public IEnumerable<OtherExpense> GenerateOtherExpenseCurrentWeekReport()
            {
                return _reportService.GenerateOtherExpenseCurrentWeekReport();
            }
            #endregion

            #region LabourCosts
            [HttpPost]
            [ActionName("GetAllLabourCostsBetweenTheSpecifiedDates")]
            public IEnumerable<LabourCost> GetAllLabourCostsBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllLabourCostsBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId);
            }

            [HttpGet]
            [ActionName("GenerateLabourCostCurrentMonthReport")]
            public IEnumerable<LabourCost> GenerateLabourCostCurrentMonthReport()
            {
                return _reportService.GenerateLabourCostCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateLabourCostTodaysReport")]
            public IEnumerable<LabourCost> GenerateLabourCostTodaysReport()
            {
                return _reportService.GenerateLabourCostTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateLabourCostCurrentWeekReport")]
            public IEnumerable<LabourCost> GenerateLabourCostCurrentWeekReport()
            {
                return _reportService.GenerateLabourCostCurrentWeekReport();
            }
            #endregion

            #region Utility
            [HttpPost]
            [ActionName("GetAllUtilityBetweenTheSpecifiedDates")]
            public IEnumerable<Utility> GetAllUtilitiesBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllUtilitiesBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId);
            }

            [HttpGet]
            [ActionName("GenerateUtilityCurrentMonthReport")]
            public IEnumerable<Utility> GenerateUtilityCurrentMonthReport()
            {
                return _reportService.GenerateUtilityCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateUtilityTodaysReport")]
            public IEnumerable<Utility> GenerateUtilityTodaysReport()
            {
                return _reportService.GenerateUtilityTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateUtilityCurrentWeekReport")]
            public IEnumerable<Utility> GenerateUtilityCurrentWeekReport()
            {
                return _reportService.GenerateUtilityCurrentWeekReport();
            }
            #endregion

            #region FlourTransfer
            [HttpPost]
            [ActionName("GetAllFlourTransfersBetweenTheSpecifiedDates")]
            public IEnumerable<FlourTransfer> GetAllFlourTransfersBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllFlourTransfersBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId);
            }

            [HttpGet]
            [ActionName("GenerateFlourTransferCurrentMonthReport")]
            public IEnumerable<FlourTransfer> GenerateFlourTransferCurrentMonthReport()
            {
                return _reportService.GenerateFlourTransferCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateFlourTransferTodaysReport")]
            public IEnumerable<FlourTransfer> GenerateFlourTransferTodaysReport()
            {
                return _reportService.GenerateFlourTransferTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateFlourTransferCurrentWeekReport")]
            public IEnumerable<FlourTransfer> GenerateFlourTransferCurrentWeekReport()
            {
                return _reportService.GenerateFlourTransferCurrentWeekReport();
            }
            #endregion

            #region MachineRepairs
            [HttpPost]
            [ActionName("GetAllMachineRepairsBetweenTheSpecifiedDates")]
            public IEnumerable<MachineRepair> GetAllMachineRepairsBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllMachineRepairsBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId);
            }

            [HttpGet]
            [ActionName("GenerateMachineRepairCurrentMonthReport")]
            public IEnumerable<MachineRepair> GenerateMachineRepairCurrentMonthReport()
            {
                return _reportService.GenerateMachineRepairCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateMachineRepairTodaysReport")]
            public IEnumerable<MachineRepair> GenerateMachineRepairTodaysReport()
            {
                return _reportService.GenerateMachineRepairTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateMachineRepairCurrentWeekReport")]
            public IEnumerable<MachineRepair> GenerateMachineRepairCurrentWeekReport()
            {
                return _reportService.GenerateMachineRepairCurrentWeekReport();
            }
            #endregion

            #region BatchOutPuts
            [HttpPost]
            [ActionName("GetAllBatchOutPutsBetweenTheSpecifiedDates")]
            public IEnumerable<BatchOutPut> GetAllBatchOutPutsBetweenTheSpecifiedDates(ReportSearch searchDates)
            {
                return _reportService.GetAllBatchOutPutsBetweenTheSpecifiedDates(searchDates.FromDate, searchDates.ToDate, searchDates.BranchId);
            }

            [HttpGet]
            [ActionName("GenerateBatchOutPutCurrentMonthReport")]
            public IEnumerable<BatchOutPut> GenerateBatchOutPutCurrentMonthReport()
            {
                return _reportService.GenerateBatchOutPutCurrentMonthReport();
            }

            [HttpGet]
            [ActionName("GenerateBatchOutPutTodaysReport")]
            public IEnumerable<BatchOutPut> GenerateBatchOutPutTodaysReport()
            {
                return _reportService.GenerateBatchOutPutTodaysReport();
            }

            [HttpGet]
            [ActionName("GenerateBatchOutPutCurrentWeekReport")]
            public IEnumerable<BatchOutPut> GenerateBatchOutPutCurrentWeekReport()
            {
                return _reportService.GenerateBatchOutPutCurrentWeekReport();
            }
            #endregion
    }
}
