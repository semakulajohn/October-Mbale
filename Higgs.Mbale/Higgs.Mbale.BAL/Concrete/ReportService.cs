using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;
using log4net;

namespace Higgs.Mbale.BAL.Concrete
{
    public class ReportService : IReportService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(ReportService));
        private IReportDataService _dataService;
        private IUserService _userService;
        private ITransactionService _transactionService;
        private ISupplyService _supplyService;
        private IAccountTransactionActivityService _accountTransactionActivityService;
       


        public ReportService(IReportDataService dataService, IUserService userService, ITransactionService transactionService,
            ISupplyService supplyService,IAccountTransactionActivityService accountTransactionActivityService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionService = transactionService;
            this._supplyService = supplyService;
            this._accountTransactionActivityService = accountTransactionActivityService;
            
        }

        #region transactions
        public IEnumerable<Transaction> GenerateTransactionCurrentMonthReport()
        {
            var results = this._dataService.GenerateTransactionCurrentMonthReport();
            var transactionList = _transactionService.MapEFToModel(results.ToList());
            return transactionList;
        }

        public IEnumerable<Transaction> GenerateTransactionCurrentWeekReport()
        {
            var results = this._dataService.GenerateTransactionCurrentWeekReport();
            var transactionList = _transactionService.MapEFToModel(results.ToList());
            return transactionList;
        }


        public IEnumerable<Transaction> GenerateTransactionTodaysReport()
        {
            var results = this._dataService.GenerateTransactionTodaysReport();
            var transactionList = _transactionService.MapEFToModel(results.ToList());
            return transactionList;
        }

        public IEnumerable<Transaction> GetAllTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate){
            var results = this._dataService.GetAllTransactionsBetweenTheSpecifiedDates(lowerSpecifiedDate,upperSpecifiedDate);
            var transactionList = _transactionService.MapEFToModel(results.ToList());
            return transactionList;
        }

      
        #endregion 

        #region accountTransactions
        public IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentMonthReport()
        {
            var results = this._dataService.GenerateAccountTransactionCurrentMonthReport();
            var accountTransactionList = _accountTransactionActivityService.MapEFToModel(results.ToList());
            return accountTransactionList;
        }

        public IEnumerable<AccountTransactionActivity> GenerateAccountTransactionTodaysReport()
        {
            var results = this._dataService.GenerateAccountTransactionTodaysReport();
            var accountTransactionList = _accountTransactionActivityService.MapEFToModel(results.ToList());
            return accountTransactionList;
        }

        public IEnumerable<AccountTransactionActivity> GenerateAccountTransactionCurrentWeekReport()
        {

            var results = this._dataService.GenerateAccountTransactionCurrentWeekReport();
            var accountTransactionList = _accountTransactionActivityService.MapEFToModel(results.ToList());
            return accountTransactionList;
        }


        public IEnumerable<AccountTransactionActivity> GetAllAccountTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string supplierId)
        {
            var results = this._dataService.GetAllAccountTransactionsBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId, supplierId);
            var accountTransactionActivityList = _accountTransactionActivityService.MapEFToModel(results.ToList());
            return accountTransactionActivityList;
        }

        #endregion


        #region supplies
        public IEnumerable<Supply> GenerateSupplyCurrentMonthReport()
        {
            var results = this._dataService.GenerateSupplyCurrentMonthReport();
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }

        public IEnumerable<Supply> GenerateSupplyCurrentWeekReport()
        {
            var results = this._dataService.GenerateSupplyCurrentWeekReport();
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }


        public IEnumerable<Supply> GenerateSupplyTodaysReport()
        {
            var results = this._dataService.GenerateSupplyTodaysReport();
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }

        public IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate,long branchId,string supplierId)
        {
            var results = this._dataService.GetAllSuppliesBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate,branchId,supplierId);
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }
        #endregion

        #region supplies for a supplier
        public IEnumerable<Supply> GenerateSupplyCurrentMonthReportForAParticularSupplier(string supplierId)
        {
            var results = this._dataService.GenerateSupplyCurrentMonthReportForAParticularSupplier(supplierId);
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }

        public IEnumerable<Supply> GenerateSupplyCurrentWeekReportForAParticularSupplier(string supplierId)
        {
            var results = this._dataService.GenerateSupplyCurrentWeekReportForAParticularSupplier(supplierId);
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }


        public IEnumerable<Supply> GenerateSupplyTodaysReportForAParticularSupplier(string supplierId)
        {
            var results = this._dataService.GenerateSupplyTodaysReportForAParticularSupplier(supplierId);
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }

        public IEnumerable<Supply> GetAllSuppliesBetweenTheSpecifiedDatesForAParticularSupplier(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate,string supplierId)
        {
            var results = this._dataService.GetAllSuppliesBetweenTheSpecifiedDatesForAParticularSupplier(lowerSpecifiedDate, upperSpecifiedDate,supplierId);
            var supplyList = _supplyService.MapEFToModel(results.ToList());
            return supplyList;
        }
        #endregion


    }
}
