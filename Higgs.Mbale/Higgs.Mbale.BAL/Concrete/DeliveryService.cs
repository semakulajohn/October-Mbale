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
using System.Configuration;
using System.IO;
using System.Text;


namespace Higgs.Mbale.BAL.Concrete
{
    public class DeliveryService: IDeliveryService
    {
        private long orderStatusIdComplete = Convert.ToInt64(ConfigurationManager.AppSettings["StatusIdComplete"]);
        private long orderStatusIdInProgress = Convert.ToInt64(ConfigurationManager.AppSettings["StatusIdInProgress"]);
        private long flourTransactionSubTypeId = Convert.ToInt64(ConfigurationManager.AppSettings["FlourSaleTransactionSubTypeId"]);
        private long branTransactionSubTypeId = Convert.ToInt64(ConfigurationManager.AppSettings["BranSaleTransactionSubTypeId"]);
        private double orderBalance = 0;
        ILog logger = log4net.LogManager.GetLogger(typeof(DeliveryService));
        private IDeliveryDataService _dataService;
        private IUserService _userService;
        private ITransactionDataService _transactionDataService;
        private ITransactionSubTypeService _transactionSubTypeService;
        private IOrderService _orderService;
        private IStockService _stockService;
        private IStockDataService _stockDataService;
        private IAccountTransactionActivityService _accountTransactionActivityService;
        private ICashService _cashService;
        private IBatchService _batchService;
        

        public DeliveryService(IDeliveryDataService dataService,IUserService userService,ITransactionDataService transactionDataService,
            ITransactionSubTypeService transactionSubTypeService,
            IOrderService orderService,IStockService stockService,IStockDataService stockDataService,
            IAccountTransactionActivityService accountTransactionActivityService,ICashService cashService,
            IBatchService batchService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionDataService = transactionDataService;
            this._transactionSubTypeService = transactionSubTypeService;
            this._orderService = orderService;
            this._stockService = stockService;
            this._stockDataService = stockDataService;
            this._accountTransactionActivityService = accountTransactionActivityService;
            this._cashService = cashService;
            this._batchService = batchService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeliveryId"></param>
        /// <returns></returns>
        public Delivery GetDelivery(long deliveryId)
        {
            var result = this._dataService.GetDelivery(deliveryId);
            return MapEFToModel(result);
        }

        public IEnumerable<Delivery> GetAllDeliveriesForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllDeliveriesForAParticularBranch(branchId);
            return MapEFToModel(results);

        }
        public IEnumerable<Delivery> GetAllDeliveriesForAParticularOrder(long orderId)
        {
            var results = this._dataService.GetAllDeliveriesForAParticularOrder(orderId);
            return MapEFToModel(results);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Delivery> GetAllDeliveries()
        {
            var results = this._dataService.GetAllDeliveries();
            return MapEFToModel(results);
        }


#region makeDelivery

        private long UpdateStoreStockDetailsOnTransfer(StoreStock storeStock)
        {
            var storeStockId = _stockService.SaveStoreStockFlourTransfer(storeStock);
            return storeStockId;

        }
        private MakeDelivery ReduceBatchStock(long batchId, long productId, long storeId, double totalQuantity, string userId)
        {
            var soldOut = false;
            MakeDelivery makeDelivery = new MakeDelivery();
            var stockToTransfer = _stockService.GetStockForAParticularBatchAndProduct(batchId, productId, storeId);
            var storeStock = _stockService.GetStoreStockForParticularStock(stockToTransfer.StockId, productId, storeId);
            if (storeStock != null)
            {
                if (storeStock.Balance == totalQuantity)
                {
                    soldOut = true;
                    orderBalance = 0;
                    var storeStockUpdate = new StoreStock()
                    {
                        StoreStockId = storeStock.StoreStockId,
                        StoreId = storeStock.StoreId,
                        StartStock = storeStock.StartStock,
                        StockId = storeStock.StockId,
                        ProductId = storeStock.ProductId,
                        StockBalance = storeStock.StockBalance,
                        BranchId = storeStock.BranchId,
                        Quantity = storeStock.Quantity,
                        SectorId = storeStock.SectorId,
                        TimeStamp = storeStock.TimeStamp,
                        InOrOut = storeStock.InOrOut,
                        Balance = 0,
                        CreatedOn = storeStock.CreatedOn,
                        SoldOut = soldOut,
                        SoldAmount = storeStock.Balance,

                    };

                   
                    makeDelivery = new MakeDelivery()
                    { 
                        StockReduced = soldOut,
                        OrderQuantityBalance = orderBalance,
                    };
                   
                    var storeStockId = UpdateStoreStockDetailsOnTransfer(storeStockUpdate);
                    return makeDelivery;
                }
                else if (storeStock.Balance < totalQuantity)
                {
                    soldOut = true;
                    var storeStockUpdate = new StoreStock()
                    {
                        StoreStockId = storeStock.StoreStockId,
                        StoreId = storeStock.StoreId,
                        StartStock = storeStock.StartStock,
                        StockId = storeStock.StockId,
                        ProductId = storeStock.ProductId,
                        StockBalance = storeStock.StockBalance,
                        BranchId = storeStock.BranchId,
                        Quantity = storeStock.Quantity,
                        SectorId = storeStock.SectorId,
                        TimeStamp = storeStock.TimeStamp,
                        InOrOut = storeStock.InOrOut,
                        Balance = 0,
                        CreatedOn = storeStock.CreatedOn,
                        SoldOut = soldOut,
                        SoldAmount = storeStock.Balance,

                    };

                    var storeStockId = UpdateStoreStockDetailsOnTransfer(storeStockUpdate);
                    orderBalance = totalQuantity - Convert.ToDouble(storeStock.Balance);
                    if (orderBalance > 0)
                    {
                        makeDelivery = new MakeDelivery()
                        {
                            StockReduced = false,
                            OrderQuantityBalance = orderBalance,
                        };
                        return makeDelivery;
                    }

                }
                else if (storeStock.Balance > totalQuantity)
                {
                    soldOut = false;
                    var stockbalance = Convert.ToDouble(storeStock.Balance) - totalQuantity;
                    orderBalance = 0;
                    var storeStockUpdate = new StoreStock()
                    {
                        StoreStockId = storeStock.StoreStockId,
                        StoreId = storeStock.StoreId,
                        StartStock = storeStock.StartStock,
                        StockId = storeStock.StockId,
                        ProductId = storeStock.ProductId,
                        StockBalance = storeStock.StockBalance,
                        BranchId = storeStock.BranchId,
                        Quantity = storeStock.Quantity,
                        SectorId = storeStock.SectorId,
                        TimeStamp = storeStock.TimeStamp,
                        InOrOut = storeStock.InOrOut,
                        Balance = stockbalance,
                        CreatedOn = storeStock.CreatedOn,
                        SoldOut = soldOut,
                        SoldAmount = totalQuantity,

                    };

                    makeDelivery = new MakeDelivery()
                    {
                        StockReduced = soldOut,
                        OrderQuantityBalance = orderBalance,
                    };

                    var storeStockId = UpdateStoreStockDetailsOnTransfer(storeStockUpdate);
                    return makeDelivery;
                   
                    
                }

            }
            return makeDelivery;
        }
       

        #region flour delivery
        private bool CheckIfStockHasOrderGrade(long orderGrade, List<long> stockGrades)
        {
            bool hasGrade = false;
           
            foreach (var stockGrade in stockGrades)
	            {
                    if (orderGrade == stockGrade)
                    {
                        hasGrade = true;
                        return hasGrade;
                    }
	            }
               
            
            return hasGrade;
        }
        

        private List<OrderGradeSize> GetOrderGradeSizes(Order order, long gradeId)
        {
            List<OrderGradeSize> orderGradeSizes = new List<OrderGradeSize>();
            foreach (var orderGrade in order.Grades)
            {
                if (orderGrade.GradeId == gradeId)
                {
                    foreach (var denomination in orderGrade.Denominations)
                    {
                        var orderGradeSize = new OrderGradeSize()
                        {
                            GradeId = orderGrade.GradeId,
                            SizeId = denomination.DenominationId,
                            Quantity = denomination.Quantity,
                        };
                        orderGradeSizes.Add(orderGradeSize);
                    }
                   
                }
            }
            return orderGradeSizes;

        }

        private void RemoveGradeSizeQuantitiesFromStore(Delivery delivery)
        {
            if (delivery.Grades != null)
            {
                if (delivery.Grades.Any())
                {

                    foreach (var grade in delivery.Grades)
                    {
                        var gradeId = grade.GradeId;
                        if (grade.Denominations != null)
                        {
                            if (grade.Denominations.Any())
                            {
                                foreach (var denomination in grade.Denominations)
                                {
                                    var inOrOut = false;
                                    //Method that removes flour from storeGradeSize table
                                    var storeGradeSize = new StoreGradeSize()
                                    {
                                        StoreId = delivery.StoreId,
                                        GradeId = gradeId,
                                        SizeId = denomination.DenominationId,
                                        Quantity = denomination.Quantity,
                                    };

                                    this._stockService.SaveStoreGradeSize(storeGradeSize, inOrOut);


                                }
                            }
                        }
                    }

                }
            }
        }

        private MakeDelivery MakeFlourDeliveryRecord(long storeId, Delivery delivery, string userId)
        {                    
            var makeDelivery = new MakeDelivery();
            if (delivery.Batches == null || !delivery.Batches.Any())
            {
                RemoveGradeSizeQuantitiesFromStore(delivery);
                makeDelivery = new MakeDelivery()
                {
                    StockReduced = true,
                    OrderQuantityBalance = 0,
                };
                return makeDelivery;
            }
            else
            {
                List<Batch> batchesList = new List<Batch>();
                foreach (var deliveryBatch in delivery.Batches)
                {
                    var batch = _batchService.GetBatch(deliveryBatch.BatchId);
                    batchesList.Add(batch);
                }

                List<Batch> SortedBatchList = batchesList.OrderBy(o => o.CreatedOn).ToList();
                foreach (var batch in SortedBatchList)
                {
                    makeDelivery = ReduceBatchStock(batch.BatchId, delivery.ProductId, delivery.StoreId, delivery.Quantity, userId);
                    if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                    {
                        makeDelivery = new MakeDelivery()
                        {
                            StockReduced = true,
                            OrderQuantityBalance = 0,
                        };
                        return makeDelivery;
                    }
                    else if (!makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                    {

                        makeDelivery = new MakeDelivery()
                        {
                            StockReduced = true,
                            OrderQuantityBalance = 0,
                        };
                        return makeDelivery;
                    }
                    else
                    {
                        if (orderBalance > 0)
                        {
                            makeDelivery = ReduceBatchStock(batch.BatchId, delivery.ProductId, delivery.StoreId, orderBalance, userId);
                        }
                    }
                }
                RemoveGradeSizeQuantitiesFromStore(delivery);
               
            }
         

          return makeDelivery;
        }
        #endregion 

        private void UpdateStoreStockDetailsOnDelivery(long storeId, long productId, string userId, bool soldOut, long stockId)
        {
            _stockService.UpdateStoreStockAndStockDetails(stockId, productId, soldOut, userId);

        }

        private MakeDelivery MakeBrandDeliveryRecord(long storeId, Delivery delivery, string userId)
        {
            var makeDelivery = new MakeDelivery();

            if (delivery.Batches == null || !delivery.Batches.Any())
            {
                return makeDelivery;
            }
            else
            {
                List<Batch> batchesList = new List<Batch>();
                foreach (var deliveryBatch in delivery.Batches)
                {
                    var batch = _batchService.GetBatch(deliveryBatch.BatchId);
                    batchesList.Add(batch);
                }

                List<Batch> SortedBatchList = batchesList.OrderBy(o => o.CreatedOn).ToList();
                foreach (var batch in SortedBatchList)
                {
                    makeDelivery = ReduceBatchStock(batch.BatchId, delivery.ProductId, delivery.StoreId, delivery.Quantity, userId);
                    if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                    {
                        makeDelivery = new MakeDelivery()
                        {
                            StockReduced = true,
                            OrderQuantityBalance = 0,
                        };
                        return makeDelivery;
                    }
                    else if (!makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                    {
                        makeDelivery = new MakeDelivery()
                        {
                            StockReduced = true,
                            OrderQuantityBalance = 0,
                        };
                        return makeDelivery;
                    }
                    else
                    {
                        if (orderBalance > 0)
                        {
                            makeDelivery = ReduceBatchStock(batch.BatchId, delivery.ProductId, delivery.StoreId, orderBalance, userId);
                        }
                    }
                }
            }
            return makeDelivery;
        }
#endregion


        public long SaveDelivery(Delivery delivery, string userId)
        {
            long deliveryId = 0;
            MakeDelivery makeDelivery = new MakeDelivery();

            if (delivery.OrderId != 0)
            {
            
                if (delivery.ProductId == 2)
                {
                    var deliveryDTO = new DTO.DeliveryDTO()
                    {
                        StoreId = delivery.StoreId,
                        CustomerId = delivery.CustomerId,
                        DeliveryCost = delivery.DeliveryCost,
                        OrderId = delivery.OrderId,
                        VehicleNumber = delivery.VehicleNumber,
                        BranchId = delivery.BranchId,
                        SectorId = delivery.SectorId,
                        PaymentModeId = delivery.PaymentModeId,
                        
                        Price = delivery.Price,
                        Quantity = delivery.Quantity,
                        ProductId = delivery.ProductId,
                        Amount = delivery.Amount,
                        Location = delivery.Location,
                        TransactionSubTypeId = delivery.TransactionSubTypeId,
                        MediaId = delivery.MediaId,
                        DeliveryId = delivery.DeliveryId,
                        DriverName = delivery.DriverName,
                        DriverNIN = delivery.DriverNIN,
                        Deleted = delivery.Deleted,
                        CreatedBy = delivery.CreatedBy,
                        CreatedOn = delivery.CreatedOn,


                    };
                     makeDelivery =   MakeBrandDeliveryRecord(delivery.StoreId, delivery,userId);
                    if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0 )
                    {
                        deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);
    
                        _orderService.UpdateOrderWithCompletedStatus(delivery.OrderId, orderStatusIdComplete, userId);

                        //if (deliveryDTO.PaymentModeId == 2 || deliveryDTO.PaymentModeId == 1)
                        //{
                        //    //generate receipt
                        // throw new   NotImplementedException();
                        //}
                        //else
                        //{
                        //    //Generate  Invoice
                        //    throw new NotImplementedException();
                        //}
                        
                    }
                    
                    else if (!makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance <= 0)
                    {
                        // no batches were selected
                        return -1;
                    }

                }
                else
                {

                    //if order.ProductId == 1
                    var deliveryDTO = new DTO.DeliveryDTO()
                    {
                        StoreId = delivery.StoreId,
                        CustomerId = delivery.CustomerId,
                        DeliveryCost = delivery.DeliveryCost,
                        OrderId = delivery.OrderId,
                        VehicleNumber = delivery.VehicleNumber,
                        BranchId = delivery.BranchId,
                        SectorId = delivery.SectorId,
                        PaymentModeId = delivery.PaymentModeId,
                      
                        Price = delivery.Price,
                        ProductId = delivery.ProductId,
                        Amount = delivery.Amount,
                        Quantity = delivery.Quantity,
                        Location = delivery.Location,
                        TransactionSubTypeId = delivery.TransactionSubTypeId,
                        MediaId = delivery.MediaId,
                        DeliveryId = delivery.DeliveryId,
                        DriverName = delivery.DriverName,
                        DriverNIN = delivery.DriverNIN,
                        Deleted = delivery.Deleted,
                        CreatedBy = delivery.CreatedBy,
                        CreatedOn = delivery.CreatedOn,


                    };
                    makeDelivery = MakeFlourDeliveryRecord(delivery.StoreId, delivery, userId);
                    if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                    {
                        deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);
                        if (delivery.Batches != null )
                        {
                            foreach (var batch in delivery.Batches)
                            {
                                var deliveryBatchDTO = new DeliveryBatchDTO()
                                {
                                    BatchId = batch.BatchId,
                                    DeliveryId = deliveryId,

                                };
                                this._dataService.SaveDeliveryBatch(deliveryBatchDTO);
                            }

                        }

                        
                        _orderService.UpdateOrderWithCompletedStatus(delivery.OrderId, orderStatusIdComplete, userId);
                        List<DeliveryGradeSize> deliveryGradeSizeList = new List<DeliveryGradeSize>();
                            
                        foreach (var grade in delivery.Grades)
                        {
                            long sizeId = 0;
                            double amount =0,price=0,quantity=0 ;
                           
                            foreach (var denomination in grade.Denominations)
	                            {
                                sizeId = denomination.DenominationId;
                                price = denomination.Price;
                                quantity = denomination.Quantity;
                                amount = (denomination.Quantity * denomination.Price);

                                var deliveryGradeSize = new DeliveryGradeSize()
                                {
                                    DeliveryId = deliveryId,
                                    GradeId = grade.GradeId,
                                    SizeId = sizeId,
                                    Quantity = quantity,
                                    Price = price,
                                    Amount = amount,
                                    TimeStamp = DateTime.Now,

                                };
                                deliveryGradeSizeList.Add(deliveryGradeSize);

		 
	                     }
                           
                        }
                        this._dataService.PurgeDeliveryGradeSize(deliveryId);
                        this.SaveDeliveryGradeSizeList(deliveryGradeSizeList);
                        //if (deliveryDTO.PaymentModeId == 2 || deliveryDTO.PaymentModeId == 1)
                        //{
                        //    //generate receipt
                        //    throw new NotImplementedException();
                        //}
                        //else
                        //{
                        //    //Generate  Invoice
                        //    throw new NotImplementedException();
                        //}
                    }
                   
                    else if (!makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance < 0)
                    {
                        // no batches were selected
                        return -1;
                    }
                   

                    long transactionSubTypeId = 0;
                    var notes = string.Empty;
                    if (delivery.ProductId == 1)
                    {
                        transactionSubTypeId = flourTransactionSubTypeId;
                         notes = "Maize Flour Sale";
                    }
                    else if(delivery.ProductId ==2)
                    {
                        transactionSubTypeId = branTransactionSubTypeId;
                         notes = "Bran Sale";
                    }
                    var paymentMode = _accountTransactionActivityService.GetPaymentMode(delivery.PaymentModeId);
                    var paymentModeName = paymentMode.Name;
                    if (paymentModeName == "Credit")
                    {
                        
                        var accountActivity = new AccountTransactionActivity()
                        {

                            AspNetUserId = delivery.CustomerId,
                            Amount = delivery.Amount,
                            Notes = notes,
                            Action = "-",
                            BranchId = delivery.BranchId,
                            TransactionSubTypeId = transactionSubTypeId,
                            SectorId = delivery.SectorId,
                            Deleted = delivery.Deleted,
                            CreatedBy = userId,
                            PaymentMode = paymentModeName,

                        };
                        var accountActivityId = this._accountTransactionActivityService.SaveAccountTransactionActivity(accountActivity, userId);

                    }
                    else if (paymentModeName == "Cash")
                    {

                        var cash = new Cash()
                        {
                       
                            Amount = delivery.Amount,
                            Notes = notes,
                            Action = "+",
                            BranchId = Convert.ToInt64(delivery.BranchId),
                            TransactionSubTypeId = transactionSubTypeId,
                            SectorId = delivery.SectorId,

                        }; 
                        _cashService.SaveCash(cash,userId);

                        
                    }
                   

                    if (delivery.Amount == 0)
                    {
                        return deliveryId;
                    }
                    else
                    {
                        long transactionTypeId = 0;
                        var transactionSubtype = _transactionSubTypeService.GetTransactionSubType(delivery.TransactionSubTypeId);
                        if (transactionSubtype != null)
                        {
                            transactionTypeId = transactionSubtype.TransactionTypeId;
                        }

                        var transaction = new TransactionDTO()
                        {
                            BranchId = delivery.BranchId,
                            SectorId = delivery.SectorId,
                            Amount = delivery.DeliveryCost,
                            TransactionSubTypeId = delivery.TransactionSubTypeId,
                            TransactionTypeId = transactionTypeId,
                            CreatedOn = DateTime.Now,
                            TimeStamp = DateTime.Now,
                            CreatedBy = userId,
                            Deleted = false,

                        };
                        var transactionId = _transactionDataService.SaveTransaction(transaction, userId);
                        return deliveryId;
                    }
                   
                  

                }
               

            }
            return deliveryId;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long deliveryId, string userId)
        {
            _dataService.MarkAsDeleted(deliveryId, userId);
        }


        void SaveDeliveryGradeSizeList(List<DeliveryGradeSize> deliveryGradeSizeList)
        {
            if (deliveryGradeSizeList != null)
            {
                if (deliveryGradeSizeList.Any())
                {
                    foreach (var deliveryGradeSize in deliveryGradeSizeList)
                    {
                        var deliveryGradeSizeDTO = new DTO.DeliveryGradeSizeDTO()
                        {
                            DeliveryId = deliveryGradeSize.DeliveryId,
                            GradeId = deliveryGradeSize.GradeId,
                            Quantity = deliveryGradeSize.Quantity,
                            SizeId = deliveryGradeSize.SizeId,
                            Price = deliveryGradeSize.Price,
                            Amount = deliveryGradeSize.Amount,
                            TimeStamp = deliveryGradeSize.TimeStamp
                        };
                        this.SaveDeliveryGradeSize(deliveryGradeSizeDTO);
                    }
                }
            }
        }
        void SaveDeliveryGradeSize(DeliveryGradeSizeDTO deliveryGradeSizeDTO)
        {
            _dataService.SaveDeliveryGradeSize(deliveryGradeSizeDTO);
        }

        private IEnumerable<DeliveryBatch> GetAllBatchesForADelivery(long deliveryId)
        {
            var results = _dataService.GetAllBatchesForADelivery(deliveryId);
            return MapEFToModel(results);
        }
       
        
        #region Mapping Methods

        public IEnumerable<Delivery> MapEFToModel(IEnumerable<EF.Models.Delivery> data)
        {
            var list = new List<Delivery>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

     
        /// <summary>
        /// Maps Delivery EF object to Delivery Model Object and
        /// returns the Delivery model object.
        /// </summary>
        /// <param name="result">EF Delivery object to be mapped.</param>
        /// <returns>Delivery Model Object.</returns>
        public Delivery MapEFToModel(EF.Models.Delivery data)
        {
            var delivery = new Delivery();

            if (data != null)
            {
                var customerName = string.Empty;
                var customer = _userService.GetAspNetUser(data.CustomerId);
                if (customer != null)
                {
                    customerName = customer.FirstName + ' ' + customer.LastName;
                }
                
                 delivery = new Delivery()
                {
                    CustomerName = customerName,
                    DeliveryCost = data.DeliveryCost,
                    OrderId = data.OrderId,
                    
                    BranchName = data.Branch != null ? data.Branch.Name : "",
                    SectorName = data.Sector != null ? data.Sector.Name : "",
                    TransactionSubTypeId = data.TransactionSubTypeId,
                    TransactionSubTypeName = data.TransactionSubType != null ? data.TransactionSubType.Name : "",
                    VehicleNumber = data.VehicleNumber,
                    BranchId = data.BranchId,
                    Location = data.Location,
                    PaymentModeId = data.PaymentModeId,
                    PaymentModeName = data.PaymentMode != null?data.PaymentMode.Name:"",
                    SectorId = data.SectorId,
                    StoreId = data.StoreId,
                    StoreName = data.Store != null ? data.Store.Name : "",
                    MediaId = data.MediaId,
                    DeliveryId = data.DeliveryId,
                    Quantity = data.Quantity,
                    DriverName = data.DriverName,
                    DriverNIN = data.DriverNIN,
                    CreatedOn = data.CreatedOn,
                    TimeStamp = data.TimeStamp,
                    Deleted = data.Deleted,
                    ProductName = data.Product != null? data.Product.Name:"",
                    Amount = data.Amount,
                    Price = data.Price,
                   
                    CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                    UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),

                };
                 var batches = GetAllBatchesForADelivery(data.DeliveryId);
                 List<DeliveryBatch> deliveryBatchList = new List<DeliveryBatch>();
                 if (batches.Any())
                 {
                     foreach (var batch in batches)
                     {
                         var deliverybatch = new DeliveryBatch()
                         {
                             BatchId = batch.BatchId,
                             BatchNumber = batch.BatchNumber,
                             DeliveryId = batch.DeliveryId,
                              BatchQuantity = batch.BatchQuantity,
       
                         };
                         deliveryBatchList.Add(deliverybatch);

                     }
                     delivery.DeliveryBatches = deliveryBatchList;
                 }
                 if (data.DeliveryGradeSizes != null)
                 {
                     if (data.DeliveryGradeSizes.Any())
                     {
                         List<Grade> grades = new List<Grade>();
                         var distinctGrades = data.DeliveryGradeSizes.GroupBy(g => g.GradeId).Select(o => o.First()).ToList();
                         foreach (var deliveryGradeSize in distinctGrades)
                         {
                             var grade = new Grade()
                             {
                                 GradeId = deliveryGradeSize.Grade.GradeId,
                                 Value = deliveryGradeSize.Grade.Value,
                                 CreatedOn = deliveryGradeSize.Grade.CreatedOn,
                                 TimeStamp = deliveryGradeSize.Grade.TimeStamp,
                                 Deleted = deliveryGradeSize.Grade.Deleted,
                                 CreatedBy = _userService.GetUserFullName(deliveryGradeSize.Grade.AspNetUser),
                                 UpdatedBy = _userService.GetUserFullName(deliveryGradeSize.Grade.AspNetUser1),
                             };
                             List<Denomination> denominations = new List<Denomination>();
                             if (deliveryGradeSize.Grade.DeliveryGradeSizes != null)
                             {
                                 if (deliveryGradeSize.Grade.DeliveryGradeSizes.Any())
                                 {
                                     var distinctSizes = deliveryGradeSize.Grade.DeliveryGradeSizes.GroupBy(s => s.SizeId).Select(o => o.First()).ToList();
                                     foreach (var ogs in distinctSizes)
                                     {
                                         var denomination = new Denomination()
                                         {
                                             DenominationId = ogs.SizeId,
                                             Value = ogs.Size != null ? ogs.Size.Value : 0,
                                             Quantity = ogs.Quantity,
                                             Price = ogs.Price,
                                         };
                                         delivery.Quantity += (ogs.Quantity * ogs.Size.Value);
                                         denominations.Add(denomination);
                                     }
                                 }
                                 grade.Denominations = denominations;
                             }
                             grades.Add(grade);
                         }
                         delivery.Grades = grades;
                     }
                 }
                return delivery;
            }
            return delivery;
        }


        public DeliveryBatch MapEFToModel(EF.Models.DeliveryBatch data)
        {
            var deliveryBatch = new DeliveryBatch()
            {

                BatchId = data.BatchId,
                DeliveryId = data.DeliveryId,
                CreatedOn = data.CreatedOn,
                BatchNumber = data.Batch != null ? data.Batch.Name : "",
                TimeStamp = data.TimeStamp,

            };
            return deliveryBatch;

        }

        public IEnumerable<DeliveryBatch> MapEFToModel(IEnumerable<EF.Models.DeliveryBatch> data)
        {
            var list = new List<DeliveryBatch>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }
       #endregion
    }
}
