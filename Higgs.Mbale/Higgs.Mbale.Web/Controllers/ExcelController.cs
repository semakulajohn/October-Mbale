using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Higgs.Mbale.Helpers;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.Web.Controllers
{
    public class ExcelController : Controller
    {
        
        private ITransactionService _transactionService;
        private IReportService _reportService;
        private ISupplyService _supplyService;
        private IAccountTransactionActivityService _accountTransactionActivityService;
        private IBatchService _batchService;
        private IDeliveryService _deliveryService;
        public ExcelController()
        {

        }

        public ExcelController(ITransactionService transactionService, IReportService reportService,
            ISupplyService supplyService,IAccountTransactionActivityService accountTransactionActivityService,
            IBatchService batchService,IDeliveryService deliveryService)
        {
            this._transactionService = transactionService;
            this._reportService = reportService;
            this._supplyService = supplyService;
            this._accountTransactionActivityService = accountTransactionActivityService;
            this._batchService = batchService;
            this._deliveryService = deliveryService;
        }
        // GET: Excel
        public ActionResult Index(int id)
        {
            int reportType = id;
            string nameOfReport = string.Empty;
            List<string> headers = new List<string>();
            headers.Add("TransactionId ");
            headers.Add("Amount");
            headers.Add("TransactionSubTypeName");
            headers.Add("CreatedOn");
            headers.Add("TransactionType");
            headers.Add("Branch");

            IEnumerable<Transaction> transactionList;
            switch (reportType)
            {

                case 1://all todays transactions
                    nameOfReport = "TodaysTransactions";
                    transactionList = _reportService.GenerateTransactionTodaysReport();
                    break;
                case 2://all this months transactions
                    nameOfReport = "CurrentMonthsTransactions";
                    transactionList = _reportService.GenerateTransactionCurrentMonthReport();
                    break;
               
                case 3://transactions for this week
                    nameOfReport = "CurrentWeeksTransactions";
                    transactionList = _reportService.GenerateTransactionCurrentWeekReport();
                   break;
               
                default://Todo:: need to decide which one is the default report data
                    transactionList = _transactionService.GetAllTransactions();
                    break;
            }
            List<List<string>> cellValues = new List<List<string>>();
            foreach (var w in transactionList)
            {
               
                var sxr = new List<string>();
                sxr.Add(w.TransactionId.ToString());
                sxr.Add(w.Amount.ToString());
                sxr.Add(w.TransactionSubTypeName);
                sxr.Add(w.CreatedOn.ToString());
                sxr.Add(w.TransactionTypeName);
                sxr.Add(w.BranchName);

                cellValues.Add(sxr);
            }
            var data = new ExcelData();
            data.Headers = headers;
            data.DataRows = cellValues;

            var file = new ExcelWriter();
            var excelFileContentInBytes = file.GenerateExcelFile(data);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nameOfReport + "_Report_" + DateTime.Now.ToString("yyyy-MM-dd-mm-ss") + ".xlsx");
            return new FileContentResult(excelFileContentInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult Supply(int id)
        {
            int reportType = id;
            string nameOfReport = string.Empty;
            List<string> headers = new List<string>();
            headers.Add("WeightNoteNumber ");
            headers.Add("Quantity");
            headers.Add("Price");
            headers.Add("Amount");
            headers.Add("BranchName");
            headers.Add("SupplyDate");
            headers.Add("TruckerNumber");
            headers.Add("SupplierName");
            headers.Add("SupplierNumber");
            headers.Add("NormalBags");
            headers.Add("StoneBags");
          

            IEnumerable<Supply> supplyList;
            switch (reportType)
            {

                case 1://all todays Supplies
                    nameOfReport = "TodaysSupplies";
                    supplyList = _reportService.GenerateSupplyTodaysReport();
                    break;
                case 2://all this months Supplies
                    nameOfReport = "CurrentMonthsSupplies";
                    supplyList = _reportService.GenerateSupplyCurrentMonthReport();
                    break;
               
                case 3://Supplies for this week
                    nameOfReport = "CurrentWeeksSupplies";
                    supplyList = _reportService.GenerateSupplyCurrentWeekReport();
                    break;
                //case 4: //supplies for a specified date
                //    nameOfReport ="SuppliesForSpecifiedDate"
                //        supplyList = _reportService.GetAllAccountTransactionsBetweenTheSpecifiedDates(DateTime lowerSpecifiedDate, DateTime upperSpecifiedDate, long branchId, string supplierId);
                default://Todo:: need to decide which one is the default report data
                    supplyList = _supplyService.GetAllSupplies();
                    break;
            }
            List<List<string>> cellValues = new List<List<string>>();
            foreach (var w in supplyList)
            {
               
                var sxr = new List<string>();
                sxr.Add(w.WeightNoteNumber.ToString());
                sxr.Add(w.Quantity.ToString());
                sxr.Add(w.Price.ToString());
                sxr.Add(w.Amount.ToString());
                sxr.Add(w.BranchName);
                sxr.Add(w.SupplyDate.ToString());
                sxr.Add(w.TruckNumber);
                sxr.Add(w.SupplierName);
                sxr.Add(w.SupplierNumber);
                sxr.Add(w.NormalBags.ToString());
                sxr.Add(w.BagsOfStones.ToString());
                

                cellValues.Add(sxr);
            }
            var data = new ExcelData();
            data.Headers = headers;
            data.DataRows = cellValues;

            var file = new ExcelWriter();
            var excelFileContentInBytes = file.GenerateExcelFile(data);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nameOfReport + "_Report_" + DateTime.Now.ToString("yyyy-MM-dd-mm-ss") + ".xlsx");
            return new FileContentResult(excelFileContentInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult SupplierSupply(int reportTypeId,string  supplierId)
        {
            int reportType = reportTypeId;
            string nameOfReport = string.Empty;
            List<string> headers = new List<string>();
            headers.Add("WeightNoteNumber ");
            headers.Add("Quantity");
            headers.Add("Price");
            headers.Add("Amount");
            headers.Add("BranchName");
            headers.Add("SupplyDate");
            headers.Add("TruckerNumber");
            headers.Add("SupplierName");
            headers.Add("SupplierNumber");
            headers.Add("NormalBags");
            headers.Add("StoneBags");


            IEnumerable<Supply> supplyList;
            switch (reportType)
            {

                case 1://all todays Supplies
                    nameOfReport = "TodaysSupplierSupplies";
                    supplyList = _reportService.GenerateSupplyTodaysReportForAParticularSupplier(supplierId);
                    break;
                case 2://all this months Supplies
                    nameOfReport = "CurrentMonthsSupplierSupplies";
                    supplyList = _reportService.GenerateSupplyCurrentMonthReportForAParticularSupplier(supplierId);
                    break;

                case 3://Supplies for this week
                    nameOfReport = "CurrentWeeksSupplierSupplies";
                    supplyList = _reportService.GenerateSupplyCurrentWeekReportForAParticularSupplier(supplierId);
                    break;

                default://Todo:: need to decide which one is the default report data
                    supplyList = _supplyService.GetAllSuppliesForAParticularSupplier(supplierId);
                    break;
            }
            List<List<string>> cellValues = new List<List<string>>();
            foreach (var w in supplyList)
            {

                var sxr = new List<string>();
                sxr.Add(w.WeightNoteNumber.ToString());
                sxr.Add(w.Quantity.ToString());
                sxr.Add(w.Price.ToString());
                sxr.Add(w.Amount.ToString());
                sxr.Add(w.BranchName);
                sxr.Add(w.SupplyDate.ToString());
                sxr.Add(w.TruckNumber);
                sxr.Add(w.SupplierName);
                sxr.Add(w.SupplierNumber);
                sxr.Add(w.NormalBags.ToString());
                sxr.Add(w.BagsOfStones.ToString());


                cellValues.Add(sxr);
            }
            var data = new ExcelData();
            data.Headers = headers;
            data.DataRows = cellValues;

            var file = new ExcelWriter();
            var excelFileContentInBytes = file.GenerateExcelFile(data);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nameOfReport + "_Report_" + DateTime.Now.ToString("yyyy-MM-dd-mm-ss") + ".xlsx");
            return new FileContentResult(excelFileContentInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

        public ActionResult AccountTransaction(int id)
        {
            int reportType = id;
            string nameOfReport = string.Empty;
            List<string> headers = new List<string>();
            headers.Add("StartAmount ");
            headers.Add("Action");
            headers.Add("Amount");
            headers.Add("Balance");
            headers.Add("Branch");
            headers.Add("Notes");
            headers.Add("CreatedOn");
            
           

            IEnumerable<AccountTransactionActivity> accountTransactionList;
            switch (reportType)
            {

                case 1://all todays account transactions
                    nameOfReport = "TodaysTransactions";
                    accountTransactionList = _reportService.GenerateAccountTransactionTodaysReport();
                    break;
                case 2://all this months account transactions
                    nameOfReport = "CurrentMonthsTransactions";
                    accountTransactionList = _reportService.GenerateAccountTransactionCurrentMonthReport();
                    break;

                case 3:// account transactions for this week
                    nameOfReport = "CurrentWeeksTransactions";
                   accountTransactionList = _reportService.GenerateAccountTransactionCurrentWeekReport();
                    break;

                default://Todo:: need to decide which one is the default report data
                    accountTransactionList = _accountTransactionActivityService.GetAllAccountTransactionActivities();
                    break;
            }
            List<List<string>> cellValues = new List<List<string>>();
            foreach (var w in accountTransactionList)
            {

                var sxr = new List<string>();
                sxr.Add(w.StartAmount.ToString());
                sxr.Add(w.Action.ToString());
                sxr.Add(w.Amount.ToString());
                sxr.Add(w.Balance.ToString());
                sxr.Add(w.BranchName);
                sxr.Add(w.Notes);
                sxr.Add(w.CreatedOn.ToString());
                cellValues.Add(sxr);
            }
            var data = new ExcelData();
            data.Headers = headers;
            data.DataRows = cellValues;

            var file = new ExcelWriter();
            var excelFileContentInBytes = file.GenerateExcelFile(data);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nameOfReport + "_Report_" + DateTime.Now.ToString("yyyy-MM-dd-mm-ss") + ".xlsx");
            return new FileContentResult(excelFileContentInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult Batch(int id)
        {
            int reportType = id;
            string nameOfReport = string.Empty;
            List<string> headers = new List<string>();
            headers.Add("BatchNumber ");
            headers.Add("Quantity");
           
            headers.Add("BranchName");
            headers.Add("CreatedOn");
            


            IEnumerable<Batch> batchList;
            switch (reportType)
            {

                case 1://all todays batches
                    nameOfReport = "TodaysBatches";
                    batchList = _reportService.GenerateBatchTodaysReport();
                    break;
                case 2://all this months batches
                    nameOfReport = "CurrentMonthsBatches";
                    batchList = _reportService.GenerateBatchCurrentMonthReport();
                    break;

                case 3://Batches for this week
                    nameOfReport = "CurrentWeeksBatches";
                    batchList = _reportService.GenerateBatchCurrentWeekReport();
                    break;
             
                default://Todo:: need to decide which one is the default report data
                    batchList = _batchService.GetAllBatches();
                    break;
            }
            List<List<string>> cellValues = new List<List<string>>();
            foreach (var w in batchList)
            {

                var sxr = new List<string>();
                sxr.Add(w.Name.ToString());
                sxr.Add(w.Quantity.ToString());
                sxr.Add(w.BranchName);
                sxr.Add(w.CreatedOn.ToString());
    
                cellValues.Add(sxr);
            }
            var data = new ExcelData();
            data.Headers = headers;
            data.DataRows = cellValues;

            var file = new ExcelWriter();
            var excelFileContentInBytes = file.GenerateExcelFile(data);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nameOfReport + "_Report_" + DateTime.Now.ToString("yyyy-MM-dd-mm-ss") + ".xlsx");
            return new FileContentResult(excelFileContentInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult Delivery(int id)
        {
            int reportType = id;
            string nameOfReport = string.Empty;
            List<string> headers = new List<string>();
            headers.Add("Location ");
            headers.Add("OrderNummber");
            headers.Add("DriverName");
            headers.Add("DriverNIN");
            headers.Add("Quantity");
            headers.Add("BranchName");
            headers.Add("BatchNumber");
            headers.Add("VehicleNumber");
            headers.Add("DeliveryCost");
            headers.Add("CustomerName");
            headers.Add("CreatedOn");
             

            IEnumerable<Delivery>  deliveryList;
            switch (reportType)
            {

                case 1://all todays Deliveries
                    nameOfReport = "TodaysDeliveries";
                    deliveryList = _reportService.GenerateDeliveryTodaysReport();
                    break;
                case 2://all this months Deliveries
                    nameOfReport = "CurrentMonthsDeliveries";
                    deliveryList = _reportService.GenerateDeliveryCurrentMonthReport();
                    break;

                case 3://Deliveries for this week
                    nameOfReport = "CurrentWeeksDeliveries";
                    deliveryList = _reportService.GenerateDeliveryCurrentWeekReport();
                    break;
               
                default://Todo:: need to decide which one is the default report data
                    deliveryList = _deliveryService.GetAllDeliveries();
                    break;
            }
            List<List<string>> cellValues = new List<List<string>>();
            foreach (var w in deliveryList)
            {

                var sxr = new List<string>();
                sxr.Add(w.Location.ToString());
                sxr.Add(w.OrderId.ToString());
                sxr.Add(w.DriverName.ToString());
                sxr.Add(w.DriverNIN.ToString());
                sxr.Add(w.Quantity.ToString());
                sxr.Add(w.BranchName.ToString());
                sxr.Add(w.BatchNumber.ToString());
                sxr.Add(w.VehicleNumber);
                sxr.Add(w.DeliveryCost.ToString());
                sxr.Add(w.CustomerName.ToString());
                sxr.Add(w.CreatedOn.ToString());


                cellValues.Add(sxr);
            }
            var data = new ExcelData();
            data.Headers = headers;
            data.DataRows = cellValues;

            var file = new ExcelWriter();
            var excelFileContentInBytes = file.GenerateExcelFile(data);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nameOfReport + "_Report_" + DateTime.Now.ToString("yyyy-MM-dd-mm-ss") + ".xlsx");
            return new FileContentResult(excelFileContentInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

      

      
    }
}