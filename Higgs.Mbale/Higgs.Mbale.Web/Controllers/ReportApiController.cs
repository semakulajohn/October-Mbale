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
    }
}
