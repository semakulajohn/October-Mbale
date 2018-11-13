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


    }
}
