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
        ILog logger = log4net.LogManager.GetLogger(typeof(DeliveryService));
        private IDeliveryDataService _dataService;
        private IUserService _userService;
        private ITransactionDataService _transactionDataService;
        private ITransactionSubTypeService _transactionSubTypeService;
        private IOrderService _orderService;
        private IStockService _stockService;
        private IStockDataService _stockDataService;
        private IAccountTransactionActivityService _accountTransactionActivityService;
        

        public DeliveryService(IDeliveryDataService dataService,IUserService userService,ITransactionDataService transactionDataService,
            ITransactionSubTypeService transactionSubTypeService,
            IOrderService orderService,IStockService stockService,IStockDataService stockDataService,IAccountTransactionActivityService accountTransactionActivityService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionDataService = transactionDataService;
            this._transactionSubTypeService = transactionSubTypeService;
            this._orderService = orderService;
            this._stockService = stockService;
            this._stockDataService = stockDataService;
            this._accountTransactionActivityService = accountTransactionActivityService;
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

        private MakeDelivery MakeFlourDeliveryRecord(long storeId, Delivery delivery, string userId)
        {
            var soldOut = false;
            var stockReduced = false;
            double balance = 0, totalBalance=0;
            List<long> orderGradeIds = new List<long>();
            List<long> stockGradeIds = new List<long>();
            List<StoreGradeSize> storeGradeSizes = new List<StoreGradeSize>();
            List<OrderGradeSize> orderGradeSizes = new List<OrderGradeSize>();
           
            var makeDelivery = new MakeDelivery();
            var storeStock = _stockService.GetStockForAParticularStoreForDelivery(storeId, delivery.ProductId,delivery.BatchId);
            var order = _orderService.GetOrder(delivery.OrderId);
            if (storeStock != null)
            {
                if (delivery.Grades != null)
                {
                    foreach (var grade in delivery.Grades)
                    {
                        orderGradeIds.Add(grade.GradeId);
                    }

                    foreach (var stockGrade in storeStock.Stock.Grades)
                    {
                        stockGradeIds.Add(stockGrade.GradeId);
                    }
                    foreach (var orderGradeId in orderGradeIds)
                    {
                        if (CheckIfStockHasOrderGrade(orderGradeId, stockGradeIds))
                        {
                            storeGradeSizes = _stockService.GetStoreGradeSizeForParticularGradeAtAStore(orderGradeId, storeStock.StoreId).ToList();
                            orderGradeSizes = GetOrderGradeSizes(order, orderGradeId);
                            if (orderGradeSizes.Any())
                            {
                                foreach (var orderGradeSize in orderGradeSizes)
                                {
                                    if(storeGradeSizes.Any())
                                    {
                                         foreach (var storeGradeSize in storeGradeSizes)
                                         {
                                             if (orderGradeSize.SizeId == storeGradeSize.SizeId)
                                             {
                                                 balance = storeGradeSize.Quantity - orderGradeSize.Quantity;
                                                 if (balance < 0)
                                                 {
                                                     //store doesn't have enough stock to cover this denomination
                                                 }
                                                 else
                                                 {
                                                     var storeGradeSizeDTO = new StoreGradeSizeDTO(){
                                                          GradeId = storeGradeSize.GradeId,
                                                          StoreId = storeGradeSize.StoreId,
                                                          SizeId = storeGradeSize.SizeId,
                                                           Quantity = orderGradeSize.Quantity,
                                                     };
                                                     _stockDataService.SaveStoreGradeSize(storeGradeSizeDTO, false);

                                                     
                                                 }
                                             }
                                           }
                                    }
                                     else
                                    {
                                        //store doesn't have that Grade
                                    }
                                   
                                }
                                
                            }
                            stockReduced = true;
                            makeDelivery = new MakeDelivery()
                            {
                                StockId = storeStock.StockId,
                                StockReduced = stockReduced,
                                OrderQuantityBalance = totalBalance,
                            };
                        }
                    }
                    return makeDelivery;
                }
                else
                {
                    return makeDelivery;
                }
               

            }
            else
            {
                return makeDelivery;
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
            var soldOut = false;
            var stockReduced = false;
            double balance = 0;
            var makeDelivery = new MakeDelivery();
            
            var storeStock = _stockService.GetStockForAParticularStoreForDelivery(storeId, delivery.ProductId,delivery.BatchId);
            if (storeStock != null)
            {

                if (storeStock.Balance == delivery.Quantity)
                {
                    soldOut = true;
                    if (storeStock.Balance != 0 || storeStock.Balance != null)
                    {
                        balance =Convert.ToDouble( storeStock.Balance) - Convert.ToDouble(delivery.Quantity);
                    }
                    else
                    {
                        balance = storeStock.Quantity - Convert.ToDouble(delivery.Quantity);
                    }
                   
                    UpdateStoreStockDetailsOnDelivery(storeId, delivery.ProductId, userId, soldOut, storeStock.StockId);
                    var storeStockObject = new StoreStock()
                    {

                        StoreId = storeStock.StoreId,
                        ProductId = storeStock.ProductId,
                        BranchId = storeStock.BranchId,
                        StockId = storeStock.StockId,
                        Quantity = storeStock.Quantity,
                        SectorId = storeStock.SectorId,
                        StockBalance = storeStock.StockBalance,
                        StartStock = storeStock.StartStock,
                        StoreStockId = storeStock.StoreStockId,
                        Balance = balance,
                        SoldAmount = delivery.Quantity,
                        CreatedOn = storeStock.CreatedOn,
                        SoldOut = soldOut,


                    };
                    _stockService.SaveStoreStock(storeStockObject, false);
                    stockReduced = true;
                     makeDelivery = new MakeDelivery(){
                        StockId = storeStock.StockId,
                        StockReduced = stockReduced,
                        OrderQuantityBalance = balance,
                    };
                    return makeDelivery;
                   
                }
                else if (storeStock.Balance > delivery.Quantity)
                {
                    if (storeStock.Balance != 0 || storeStock.Balance != null)
                    {
                        balance = Convert.ToDouble(storeStock.Balance) - Convert.ToDouble(delivery.Quantity);
                    }
                    else
                    {
                        balance = storeStock.Quantity - Convert.ToDouble(delivery.Quantity);
                    }
                   
                    var storeStockObject = new StoreStock()
                    {
                        StoreId = storeStock.StoreId,
                        ProductId = storeStock.ProductId,
                        BranchId = storeStock.BranchId,
                        StockId = storeStock.StockId,
                        Quantity = storeStock.Quantity,
                        SectorId = storeStock.SectorId,
                        StoreStockId = storeStock.StoreStockId,
                        StockBalance = storeStock.StockBalance,
                        StartStock = storeStock.StartStock,
                        Balance = balance,
                        SoldAmount = delivery.Quantity,
                        CreatedOn = storeStock.CreatedOn,
                        SoldOut = storeStock.SoldOut,

                    };
                    _stockService.SaveStoreStock(storeStockObject, false);
                    stockReduced = true;
                      makeDelivery = new MakeDelivery(){
                        StockId = storeStock.StockId,
                        StockReduced = stockReduced,
                        OrderQuantityBalance = 0,
                    };
                    return makeDelivery;
                   
                }
                else
                {
                    double stockBalance = 0,deliverQuantity= 0,newOrderQuantity = 0;

                    if (storeStock.Balance != 0 || storeStock.Balance != null)
                    {
                        balance = Convert.ToDouble(storeStock.Balance) - Convert.ToDouble(delivery.Quantity);
                    }
                    else
                    {
                        balance = storeStock.Quantity - Convert.ToDouble(delivery.Quantity);
                    }
                    if (stockBalance < 0)
                    {
                        balance = 0;
                        deliverQuantity = storeStock.Quantity;
                        newOrderQuantity = Convert.ToDouble(delivery.Quantity) - storeStock.Quantity;
                    }

                    soldOut = true;
                    UpdateStoreStockDetailsOnDelivery(storeId, delivery.ProductId, userId, soldOut, storeStock.StockId);
                    var storeStockObject = new StoreStock()
                    {

                        StoreId = storeStock.StoreId,
                        ProductId = storeStock.ProductId,
                        BranchId = storeStock.BranchId,
                        StockId = storeStock.StockId,
                        Quantity = storeStock.Quantity,
                        SectorId = storeStock.SectorId,
                        StoreStockId = storeStock.StoreStockId,
                        StockBalance = storeStock.StockBalance,
                        StartStock = storeStock.StartStock,
                        Balance = balance,
                        SoldAmount = deliverQuantity,
                        CreatedOn = storeStock.CreatedOn,
                        SoldOut = soldOut,


                    };
                    _stockService.SaveStoreStock(storeStockObject, false);
                    stockReduced = true;
                       makeDelivery = new MakeDelivery(){
                        StockId = storeStock.StockId,
                        StockReduced = stockReduced,
                        OrderQuantityBalance = stockBalance,
                    };
                    return makeDelivery;
                   
                                
            }
               
            }
            else
            {
                // we dont have stock in store
                stockReduced = false;
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
                //var order = _orderService.GetOrder(delivery.OrderId);
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
                        BatchId = delivery.BatchId,
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
                    if (makeDelivery.StockReduced && (makeDelivery.OrderQuantityBalance == 0 || makeDelivery.OrderQuantityBalance > 0))
                    {
                        deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);


                        //StringBuilder sb = new StringBuilder();
                        //string strNewPath = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["MbaleReceipt"]);
                        //using (StreamReader sr = new StreamReader(strNewPath))
                        //{
                        //    while (!sr.EndOfStream)
                        //    {
                        //        sb.Append(sr.ReadLine());
                        //    }
                        //}

                        //string body = sb.ToString().Replace("#CUSTOMERNAME#", deliveryDTO.CustomerId);
                        //body = body.Replace("#AMOUNT#", deliveryDTO.Amount);


                        //try
                        //{

                        //}
                        //catch (Exception ex)
                        //{

                        //    logger.Debug("receipt Not generated: " + ex.Message);
                        //}


                        var deliveryStockDTO = new DeliveryStockDTO()
                        {
                            StockId = makeDelivery.StockId,
                            DeliveryId = deliveryId,

                        };
                        _dataService.SaveDeliveryStock(deliveryStockDTO);
                        _orderService.UpdateOrderWithCompletedStatus(delivery.OrderId, orderStatusIdComplete, userId);
                    }
                    else if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance < 0)
                    {
                        deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);
                        if (makeDelivery.OrderQuantityBalance != 0)
                        {
                            var deliveryStockDTO = new DeliveryStockDTO()
                            {
                                StockId = makeDelivery.StockId,
                                DeliveryId = deliveryId,

                            };
                            _dataService.SaveDeliveryStock(deliveryStockDTO);
                            _orderService.UpdateOrderWithInProgressStatus(delivery.OrderId, orderStatusIdInProgress, userId);
                           
                        }

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
                        BatchId = delivery.BatchId,
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
                        var deliveryStockDTO = new DeliveryStockDTO()
                        {
                            StockId = makeDelivery.StockId,
                            DeliveryId = deliveryId,
                        };
                        _dataService.SaveDeliveryStock(deliveryStockDTO);
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
                    }
                    else
                    {
                        deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);
                        if (makeDelivery.OrderQuantityBalance != 0)
                        {
                            var deliveryStockDTO = new DeliveryStockDTO()
                            {
                                StockId = makeDelivery.StockId,
                                DeliveryId = deliveryId,

                            };
                            _dataService.SaveDeliveryStock(deliveryStockDTO);
                            _orderService.UpdateOrderWithInProgressStatus(delivery.OrderId, orderStatusIdInProgress, userId);

                            List<DeliveryGradeSize> deliveryGradeSizeList = new List<DeliveryGradeSize>();

                            foreach (var grade in delivery.Grades)
                            {
                                long sizeId = 0;
                                double amount = 0, price = 0, quantity = 0;

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
                        }

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

        #region Mapping Methods

        private IEnumerable<Delivery> MapEFToModel(IEnumerable<EF.Models.Delivery> data)
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
                    BatchId = data.BatchId,
                    DriverName = data.DriverName,
                    DriverNIN = data.DriverNIN,
                    CreatedOn = data.CreatedOn,
                    TimeStamp = data.TimeStamp,
                    Deleted = data.Deleted,
                    ProductName = data.Product != null? data.Product.Name:"",
                    Amount = data.Amount,
                    Price = data.Price,
                    BatchNumber = data.Batch != null? data.Batch.Name:"",
                    CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                    UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),

                };
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



       #endregion
    }
}
