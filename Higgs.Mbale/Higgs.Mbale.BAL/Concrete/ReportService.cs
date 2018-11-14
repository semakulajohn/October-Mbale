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
        private IBatchService _batchService;
        private IDeliveryService _deliveryService;
        private ICashService _cashService;
        private IOrderService _orderService;
        private ILabourCostService _labourCostService;
        private IOtherExpenseService _otherExpenseService;
        private IFactoryExpenseService _factoryExpenseService;
        private IBatchOutPutService _batchOutPutService;
        private IFlourTransferService _flourTransferService;
        private IMachineRepairService _machineRepairService;
        private IUtilityService _utilityService;
       


        public ReportService(IReportDataService dataService, IUserService userService, ITransactionService transactionService,
            ISupplyService supplyService,IAccountTransactionActivityService accountTransactionActivityService,
            IBatchService batchService,IDeliveryService deliveryService,ICashService cashService,IOrderService orderService,
            ILabourCostService labourCostService,IOtherExpenseService otherExpenseService,IFactoryExpenseService factoryExpenseService,
            IBatchOutPutService batchOutPutService,IFlourTransferService flourTransferService,IMachineRepairService machineRepairService,
            IUtilityService utilityService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionService = transactionService;
            this._supplyService = supplyService;
            this._accountTransactionActivityService = accountTransactionActivityService;
            this._batchService = batchService;
            this._deliveryService = deliveryService;
            this._cashService = cashService;
            this._orderService = orderService;
            this._labourCostService = labourCostService;
            this._otherExpenseService = otherExpenseService;
            this._factoryExpenseService = factoryExpenseService;
            this._batchOutPutService = batchOutPutService;
            this._flourTransferService = flourTransferService;
            this._machineRepairService = machineRepairService;
            this._utilityService = utilityService;
            
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

        #region batches

        public IEnumerable<Batch> GenerateBatchCurrentMonthReport()
        {
            var results = this._dataService.GenerateBatchCurrentMonthReport();
            var batchList = _batchService.MapEFToModel(results.ToList());
            return batchList;
        }

        public IEnumerable<Batch> GenerateBatchCurrentWeekReport()
        {
            var results = this._dataService.GenerateBatchCurrentWeekReport();
            var batchList = _batchService.MapEFToModel(results.ToList());
            return batchList;
        }


        public IEnumerable<Batch> GenerateBatchTodaysReport()
        {
            var results = this._dataService.GenerateBatchTodaysReport();
            var batchList = _batchService.MapEFToModel(results.ToList());
            return batchList;
        }

        public IEnumerable<Batch> GetAllBatchesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
        {
            var results = this._dataService.GetAllBatchesBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId);
            var batchList = _batchService.MapEFToModel(results.ToList());
            return batchList;
        }
      
        #endregion

        #region deliveries
        public IEnumerable<Delivery> GenerateDeliveryCurrentMonthReport()
        {
            var results = this._dataService.GenerateDeliveryCurrentMonthReport();
            var deliveryList = _deliveryService.MapEFToModel(results.ToList());
            return deliveryList;
        }

        public IEnumerable<Delivery> GenerateDeliveryCurrentWeekReport()
        {
            var results = this._dataService.GenerateDeliveryCurrentWeekReport();
            var deliveryList = _deliveryService.MapEFToModel(results.ToList());
            return deliveryList;
        }


        public IEnumerable<Delivery> GenerateDeliveryTodaysReport()
        {
            var results = this._dataService.GenerateDeliveryTodaysReport();
            var deliveryList = _deliveryService.MapEFToModel(results.ToList());
            return deliveryList;
        }

        public IEnumerable<Delivery> GetAllDeliveriesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string customerId)
        {
            var results = this._dataService.GetAllDeliveriesBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId, customerId);
            var deliveryList = _deliveryService.MapEFToModel(results.ToList());
            return deliveryList;
        }
        #endregion

        #region Cash

        public IEnumerable<Cash> GenerateCashCurrentMonthReport()
        {
            var results = this._dataService.GenerateCashCurrentMonthReport();
            var cashList = _cashService.MapEFToModel(results.ToList());
            return cashList;
        }

        public IEnumerable<Cash> GenerateCashCurrentWeekReport()
        {
            var results = this._dataService.GenerateCashCurrentWeekReport();
            var cashList = _cashService.MapEFToModel(results.ToList());
            return cashList;
        }


        public IEnumerable<Cash> GenerateCashTodaysReport()
        {
            var results = this._dataService.GenerateCashTodaysReport();
            var cashList = _cashService.MapEFToModel(results.ToList());
            return cashList;
        }

        public IEnumerable<Cash> GetAllCashBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
        {
            var results = this._dataService.GetAllCashBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId);
            var cashList = _cashService.MapEFToModel(results.ToList());
            return cashList;
        }

        #endregion

        #region  Factory Expenses
        public IEnumerable<FactoryExpense> GetAllFactoryExpensesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
        {
            var results = this._dataService.GetAllFactoryExpensesBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId);
            var factoryExpenseList = _factoryExpenseService.MapEFToModel(results.ToList());
            return factoryExpenseList;
        }

        public IEnumerable<FactoryExpense> GenerateFactoryExpenseCurrentMonthReport()
        {
            var results = this._dataService.GenerateFactoryExpenseCurrentMonthReport();
            var factoryExpenseList = _factoryExpenseService.MapEFToModel(results.ToList());
            return factoryExpenseList;
        }

        public IEnumerable<FactoryExpense> GenerateFactoryExpenseTodaysReport()
        {
            var results = this._dataService.GenerateFactoryExpenseTodaysReport();
            var factoryExpenseList = _factoryExpenseService.MapEFToModel(results.ToList());
            return factoryExpenseList;
        }

        public IEnumerable<FactoryExpense> GenerateFactoryExpenseCurrentWeekReport()
        {

            var results = this._dataService.GenerateFactoryExpenseCurrentWeekReport();
            var factoryExpenseList = _factoryExpenseService.MapEFToModel(results.ToList());
            return factoryExpenseList;
        }

        #endregion

        #region  Other Expenses
        public IEnumerable<OtherExpense> GetAllOtherExpensesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
        {
            var results = this._dataService.GetAllOtherExpensesBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId);
            var otherExpenseList = _otherExpenseService.MapEFToModel(results.ToList());
            return otherExpenseList;
        }

        public IEnumerable<OtherExpense> GenerateOtherExpenseCurrentMonthReport()
        {
            var results = this._dataService.GenerateOtherExpenseCurrentMonthReport();
            var otherExpenseList = _otherExpenseService.MapEFToModel(results.ToList());
            return otherExpenseList;
        }

        public IEnumerable<OtherExpense> GenerateOtherExpenseTodaysReport()
        {
            var results = this._dataService.GenerateOtherExpenseTodaysReport();
            var otherExpenseList = _otherExpenseService.MapEFToModel(results.ToList());
            return otherExpenseList;
        }

        public IEnumerable<OtherExpense> GenerateOtherExpenseCurrentWeekReport()
        {
            var results = this._dataService.GenerateOtherExpenseCurrentWeekReport();
            var otherExpenseList = _otherExpenseService.MapEFToModel(results.ToList());
            return otherExpenseList;
        }

        #endregion

        #region  batchoutputs
        public IEnumerable<BatchOutPut> GetAllBatchOutPutsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
        {
            var results = this._dataService.GetAllBatchOutPutsBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId);
            var batchOutPutList = _batchOutPutService.MapEFToModel(results.ToList());
            return batchOutPutList;
        }

        public IEnumerable<BatchOutPut> GenerateBatchOutPutCurrentMonthReport()
        {
            var results = this._dataService.GenerateBatchOutPutCurrentMonthReport();
            var batchOutPutList = _batchOutPutService.MapEFToModel(results.ToList());
            return batchOutPutList;
        }

        public IEnumerable<BatchOutPut> GenerateBatchOutPutTodaysReport()
        {
            var results = this._dataService.GenerateBatchOutPutTodaysReport();
            var batchOutPutList = _batchOutPutService.MapEFToModel(results.ToList());
            return batchOutPutList;
        }

        public IEnumerable<BatchOutPut> GenerateBatchOutPutCurrentWeekReport()
        {

            var results = this._dataService.GenerateBatchOutPutCurrentWeekReport();
            var batchOutPutList = _batchOutPutService.MapEFToModel(results.ToList());
            return batchOutPutList;
        }

        #endregion

        #region  LabourCosts
        public IEnumerable<LabourCost> GetAllLabourCostsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
        {
            var results = this._dataService.GetAllLabourCostsBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId);
            var labourCostList = _labourCostService.MapEFToModel(results.ToList());
            return labourCostList;
        }

        public IEnumerable<LabourCost> GenerateLabourCostCurrentMonthReport()
        {
            var results = this._dataService.GenerateLabourCostCurrentMonthReport();
            var labourCostList = _labourCostService.MapEFToModel(results.ToList());
            return labourCostList;
        }

        public IEnumerable<LabourCost> GenerateLabourCostTodaysReport()
        {
            var results = this._dataService.GenerateLabourCostTodaysReport();
            var labourCostList = _labourCostService.MapEFToModel(results.ToList());
            return labourCostList;
        }

        public IEnumerable<LabourCost> GenerateLabourCostCurrentWeekReport()
        {

            var results = this._dataService.GenerateLabourCostCurrentWeekReport();
            var labourCostList = _labourCostService.MapEFToModel(results.ToList());
            return labourCostList;
        }

        #endregion

        #region  MachineRepair
        public IEnumerable<MachineRepair> GetAllMachineRepairsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
        {
            var results = this._dataService.GetAllMachineRepairsBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId);
            var machineRepairList = _machineRepairService.MapEFToModel(results.ToList());
            return machineRepairList;
        }

        public IEnumerable<MachineRepair> GenerateMachineRepairCurrentMonthReport()
        {
            var results = this._dataService.GenerateMachineRepairCurrentMonthReport();
            var machineRepairList = _machineRepairService.MapEFToModel(results.ToList());
            return machineRepairList;
        }

        public IEnumerable<MachineRepair> GenerateMachineRepairTodaysReport()
        {
            var results = this._dataService.GenerateMachineRepairTodaysReport();
            var machineRepairList = _machineRepairService.MapEFToModel(results.ToList());
            return machineRepairList;
        }

        public IEnumerable<MachineRepair> GenerateMachineRepairCurrentWeekReport()
        {

            var results = this._dataService.GenerateMachineRepairCurrentWeekReport();
            var machineRepairList = _machineRepairService.MapEFToModel(results.ToList());
            return machineRepairList;
        }

        #endregion

        #region  Utility
        public IEnumerable<Utility> GetAllUtilitiesBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
        {
            var results = this._dataService.GetAllUtilitiesBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId);
            var utilityList = _utilityService.MapEFToModel(results.ToList());
            return utilityList;
        }

        public IEnumerable<Utility> GenerateUtilityCurrentMonthReport()
        {
            var results = this._dataService.GenerateUtilityCurrentMonthReport();
            var utilityList = _utilityService.MapEFToModel(results.ToList());
            return utilityList;
        }

        public IEnumerable<Utility> GenerateUtilityTodaysReport()
        {
            var results = this._dataService.GenerateUtilityTodaysReport();
            var utilityList = _utilityService.MapEFToModel(results.ToList());
            return utilityList;
        }

        public IEnumerable<Utility> GenerateUtilityCurrentWeekReport()
        {

            var results = this._dataService.GenerateUtilityCurrentWeekReport();
            var utilityList = _utilityService.MapEFToModel(results.ToList());
            return utilityList;
        }

        #endregion


        #region  FlourTransfer
        public IEnumerable<FlourTransfer> GetAllFlourTransfersBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId)
        {
            var results = this._dataService.GetAllFlourTransfersBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId);
            var flourTransferList = _flourTransferService.MapEFToModel(results.ToList());
            return flourTransferList;
        }

        public IEnumerable<FlourTransfer> GenerateFlourTransferCurrentMonthReport()
        {
            var results = this._dataService.GenerateFlourTransferCurrentMonthReport();
            var flourTransferList = _flourTransferService.MapEFToModel(results.ToList());
            return flourTransferList;
        }

        public IEnumerable<FlourTransfer> GenerateFlourTransferTodaysReport()
        {
            var results = this._dataService.GenerateFlourTransferTodaysReport();
            var flourTransferList = _flourTransferService.MapEFToModel(results.ToList());
            return flourTransferList;    
        }

        public IEnumerable<FlourTransfer> GenerateFlourTransferCurrentWeekReport()
        {

            var results = this._dataService.GenerateFlourTransferCurrentWeekReport();
            var flourTransferList = _flourTransferService.MapEFToModel(results.ToList());
            return flourTransferList;
        }

        #endregion

        #region orders
        public IEnumerable<Order> GenerateOrderCurrentMonthReport()
        {
            var results = this._dataService.GenerateOrderCurrentMonthReport();
            var orderList = _orderService.MapEFToModel(results.ToList());
            return orderList;
        }

        public IEnumerable<Order> GenerateOrderCurrentWeekReport()
        {
            var results = this._dataService.GenerateOrderCurrentWeekReport();
            var orderList = _orderService.MapEFToModel(results.ToList());
            return orderList;
        }


        public IEnumerable<Order> GenerateOrderTodaysReport()
        {
            var results = this._dataService.GenerateOrderTodaysReport();
            var orderList = _orderService.MapEFToModel(results.ToList());
            return orderList;
        }

        public IEnumerable<Order> GetAllOrdersBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string customerId)
        {
            var results = this._dataService.GetAllOrdersBetweenTheSpecifiedDates(lowerSpecifiedDate, upperSpecifiedDate, branchId, customerId);
            var orderList = _orderService.MapEFToModel(results.ToList());
            return orderList;
        }
        #endregion
    }
}
