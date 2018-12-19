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
        private long invoiceId = Convert.ToInt64(ConfigurationManager.AppSettings["Invoice"]);
        private long receiptId = Convert.ToInt64(ConfigurationManager.AppSettings["Receipt"]);
        private double orderBalance = 0,batchBrandBalance=0,soldOutAmount =0;
        private bool hasBalance = false;
        List<BatchDeliveryDetails> batchDeliveryList = new List<BatchDeliveryDetails>();
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
        private IDocumentService _documentService;
        

        public DeliveryService(IDeliveryDataService dataService,IUserService userService,ITransactionDataService transactionDataService,
            ITransactionSubTypeService transactionSubTypeService,IDocumentService documentService,
            IOrderService orderService,IStockService stockService,IStockDataService stockDataService,
            IAccountTransactionActivityService accountTransactionActivityService,ICashService cashService,
            IBatchService batchService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionDataService = transactionDataService;
            this._transactionSubTypeService = transactionSubTypeService;
            this._documentService = documentService;
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

       

        #region flour 
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

        private bool CheckIfBatchHasOrderGrade(long orderGrade, List<Grade> deliveryGrades)
        {
            bool hasGrade = false;
            List<long> gradeIds = new List<long>();
            foreach (var grade in deliveryGrades)
            {
                long gradeId = grade.GradeId;
                gradeIds.Add(gradeId);
            }

            foreach (var foundGradeId in gradeIds)
            {
                if (orderGrade == foundGradeId)
                {
                    hasGrade = true;
                    return hasGrade;
                }
            }
            return hasGrade;
        }
        private long UpdateStoreStockDetailsOnTransfer(StoreStock storeStock)
        {
            var storeStockId = _stockService.SaveStoreStockFlourTransfer(storeStock);
            return storeStockId;

        }
       

        private MakeDelivery ReduceBatchFlourStock(Delivery delivery,List<BatchOutPut> batchOutPuts, string userId)
        {
            BatchOutPut batchOutPut = new BatchOutPut();
            if(batchOutPuts.Any())
            {
                batchOutPut = batchOutPuts.First();
            }
            
            bool hasGrade = false;
           var soldOut = false;
            foreach (var deliveryGrade in batchOutPut.Grades)
	            {
                   
	              List<OrderGradeSize> orderGradeSizes = new List<OrderGradeSize>();
                  List<BatchGradeSize> batchGradeSizes = new List<BatchGradeSize>();
                 
                 double quantityToRemove = 0;
   
           var order = _orderService.GetOrder(delivery.OrderId);
            foreach (var orderGrade in order.Grades)
	            {
               hasGrade = CheckIfBatchHasOrderGrade(orderGrade.GradeId,batchOutPut.Grades);
                if(hasGrade)
                {
                     if (deliveryGrade.Denominations != null)
                        {
                            if (deliveryGrade.Denominations.Any())
                            {
                                foreach (var denominationDelivery in deliveryGrade.Denominations)
                                {
                                    foreach (var denom in deliveryGrade.Denominations)
                                    {
                                        quantityToRemove = denominationDelivery.QuantityToRemove;
                                        var batchGradeSizeBalance = denom.Balance - denominationDelivery.QuantityToRemove;
                                        var batchGradeSize = new BatchGradeSize()
                                                {
                                                    BatchOutPutId = batchOutPut.BatchOutPutId,
                                                    GradeId = deliveryGrade.GradeId,
                                                    SizeId = denom.DenominationId,
                                                    Quantity = denom.Quantity,
                                                    Balance = batchGradeSizeBalance,
                                                };

                                        batchGradeSizes.Add(batchGradeSize);
                                        var inOrOut = false;
                                        //Method that removes flour from storeGradeSize table
                                        var storeGradeSize = new StoreGradeSize()
                                        {
                                            StoreId = delivery.StoreId,
                                            GradeId = orderGrade.GradeId,
                                            SizeId = denom.DenominationId,
                                            Quantity = quantityToRemove,
                                        };

                                        this._stockService.SaveStoreGradeSize(storeGradeSize, inOrOut);
                                    }
                                }
                                _batchService.UpdateBatchGradeSizes(batchGradeSizes); 
                                     if(orderGrade.Denominations != null)
                                            {
                                                 if(orderGrade.Denominations.Any())
                                                     {
                                                     foreach (var denom in orderGrade.Denominations)
	                                                     {
                                                             var orderGradeBalance = denom.Balance - quantityToRemove;
		                                             var orderGradeSize = new OrderGradeSize()
                                                             {
                                                              OrderId = delivery.OrderId,
                                                                 GradeId = orderGrade.GradeId,
                                                                SizeId = denom.DenominationId,
                                                                 Quantity = denom.Quantity,
                                                                 Balance = orderGradeBalance,
                                                              };

                                                 orderGradeSizes.Add(orderGradeSize);
                                    if(orderGradeBalance > 0)
                                    {
                                        hasBalance = true;
                                    }
                                   
                                            
	                          }
                           }
                         }
               
                                _orderService.SaveOrderGradeSizeList(orderGradeSizes);
                    }
                    }
                 }
                }
            }
            if(hasBalance){
            soldOut = false;   
            }
            MakeDelivery makeDelivery = new MakeDelivery()
            {
                StockReduced = soldOut,

            };
            return makeDelivery;
        }

        private Grade GetGradeSameAsOrderGrade(List<Grade> deliveryGrades, long orderGradeId)
        {
            var gradeFound = new Grade();
            List<long> gradeIds = new List<long>();
            foreach (var grade in deliveryGrades)
            {
                long gradeId = grade.GradeId;
                gradeIds.Add(gradeId);
            }

            foreach (var foundGradeId in gradeIds)
            {
                if (orderGradeId == foundGradeId)
                {
                    foreach (var grade in deliveryGrades)
                    {
                        if(grade.GradeId == foundGradeId)
                        {
                            gradeFound = grade;
                            
                            
                        }
                    }
                }
            }
            return gradeFound;
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

      
        private bool FindIfFlourOrderHasBalance(List<OrderGradeSize> orderGradeSizeList)
        {
           bool hasBalance = false;
            foreach (var denomBalance in orderGradeSizeList)
            {
                if (denomBalance.Balance > 0)
                {
                    hasBalance = true;
                    return hasBalance;
                }
            }
            return hasBalance;            
        }
       
        private MakeDelivery MakeFlourDeliveryRecord(Delivery delivery, string userId)
        {
            bool orderHasBalance = false;
            Grade foundGrade = new Grade();
            var makeDelivery = new MakeDelivery();
            if (delivery.Batches == null || !delivery.Batches.Any())
            {
                if(delivery.SelectedDeliveryGrades.Any())
                {   
                   
                    bool hasGrade = false;
                  List<OrderGradeSize> orderGradeSizes = new List<OrderGradeSize>();
                var order = _orderService.GetOrder(delivery.OrderId);
                     foreach (var orderGrade in order.Grades)
	                         {
                                 hasGrade = CheckIfBatchHasOrderGrade(orderGrade.GradeId, delivery.SelectedDeliveryGrades);
                                     if(hasGrade)
                                     {
                                  foundGrade =  GetGradeSameAsOrderGrade(delivery.SelectedDeliveryGrades,  orderGrade.GradeId);
                                  int j = 0;
                                  foreach (var foundGradeDenom in foundGrade.Denominations)
                                  {
                                      //int j = 0;
                                      //int count = orderGrade.Denominations.Count();
                                      double quantityToRemove = 0;
                                      bool flag = false;
                                      quantityToRemove = foundGradeDenom.Quantity;
                                      double balanceValue = 0,quantity = 0;
                                      for (int i = j; i < orderGrade.Denominations.Count(); i++)
			                                {
                                                j = j + 1;
                                                balanceValue = orderGrade.Denominations[i].Balance;
                                                quantity = orderGrade.Denominations[i].Quantity;
			                                
                                      
                                  //}
                                      //foreach (var denom in orderGrade.Denominations)
                                      //{
                                         
                                          //var orderGradeSizeBalance =denom.Balance- quantityToRemove;
                                      var orderGradeSizeBalance =balanceValue - quantityToRemove;
                                          var orderGradeSize = new OrderGradeSize()
                                            {
                                                OrderId = delivery.OrderId,
                                                GradeId = orderGrade.GradeId,
                                                SizeId = foundGradeDenom.DenominationId,
                                                Quantity = quantity,
                                                Balance = orderGradeSizeBalance,
                                            };

                                          orderGradeSizes.Add(orderGradeSize);

                                          var inOrOut = false;
                                          //Method that removes flour from storeGradeSize table
                                          var storeGradeSize = new StoreGradeSize()
                                          {
                                              StoreId = delivery.StoreId,
                                              GradeId = orderGrade.GradeId,
                                              SizeId = foundGradeDenom.DenominationId,
                                              Quantity = quantityToRemove,
                                          };

                                          this._stockService.SaveStoreGradeSize(storeGradeSize, inOrOut);
                                      //}
                                          if (i < 6)
                                          {
                                              flag = true;
                                              break;
                                          }
                                            }
                                      if (flag)
                                      {
                                          continue;
                                      }
                                     }
                                 orderHasBalance =  FindIfFlourOrderHasBalance(orderGradeSizes);
                                 _orderService.PurgeOrderGradeSize(delivery.OrderId);
                                 //_orderService.SaveOrderGradeSizeList(orderGradeSizes);
                                 foreach (var item in orderGradeSizes)
                                 {
                                     var orderGradeSize = new OrderGradeSize()
                                     {
                                         OrderId = item.OrderId,
                                         GradeId = item.GradeId,
                                         SizeId = item.SizeId,
                                         Quantity = item.Quantity,
                                         Balance = item.Balance

                                     };
                                     _orderService.UpdateOrderGradeSizes(orderGradeSize.OrderId, orderGradeSize.GradeId, orderGradeSize.SizeId, orderGradeSize.Quantity,Convert.ToDouble(orderGradeSize.Balance));
                                 }
                                      }
                                 }
                     if (orderHasBalance)
                     {
                         makeDelivery = new MakeDelivery()
                         {
                             StockReduced = true,
                             OrderQuantityBalance = 1,
                         };
                         return makeDelivery;
                     }
                     else
                     {
                         makeDelivery = new MakeDelivery()
                         {
                             StockReduced = true,
                             OrderQuantityBalance = 0,
                         };
                         return makeDelivery;
                     }
                       
                 }

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
                    makeDelivery = ReduceBatchFlourStock(delivery,batch.BatchOutPuts,userId);
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
                            makeDelivery = ReduceBatchFlourStock(delivery,batch.BatchOutPuts, userId);
                        }
                    }
                }
                

            }


            return makeDelivery;
        }
        #endregion 

            
        #region Brand

        private MakeDelivery ReduceBatchBrandStock(Batch batch, double orderQuantity, double totalQuantity, string userId)
        {
            var soldOut = false;
            MakeDelivery makeDelivery = new MakeDelivery();
            if (orderQuantity >= totalQuantity)
            {

                if (batch != null)
                {
                    if (orderQuantity >= totalQuantity)
                    {
                        if (totalQuantity > batch.BrandBalance)
                        {
                            orderBalance = orderQuantity - batch.BrandBalance;
                        }
                        else
                        {
                            orderBalance = orderQuantity - totalQuantity;
                        }

                        if (totalQuantity <= batch.BrandBalance)
                        {
                            batchBrandBalance = batch.BrandBalance - totalQuantity;
                            _batchService.UpdateBatchBrandBalance(batch.BatchId, batchBrandBalance, userId);
                            if (batchBrandBalance > 0)
                            {

                                makeDelivery = new MakeDelivery()
                                {
                                    StockReduced = soldOut,
                                    OrderQuantityBalance = orderBalance,
                                    BatchBrandBalance = batchBrandBalance,

                                };



                                return makeDelivery;
                            }
                            else
                            {

                                makeDelivery = new MakeDelivery()
                                {
                                    StockReduced = true,
                                    OrderQuantityBalance = orderBalance,
                                    BatchBrandBalance = batchBrandBalance,

                                };
                                return makeDelivery;
                            }

                        }
                        else
                        {
                            batchBrandBalance = 0;
                            _batchService.UpdateBatchBrandBalance(batch.BatchId, batchBrandBalance, userId);
                            orderBalance = orderQuantity - batch.BrandBalance;


                            makeDelivery = new MakeDelivery()
                            {
                                StockReduced = true,
                                OrderQuantityBalance = orderBalance,
                                BatchBrandBalance = batchBrandBalance,

                            };
                            return makeDelivery;
                        }

                    }
                    else
                    {

                        batchBrandBalance = batch.BrandBalance - orderQuantity;
                        orderBalance = batchBrandBalance - orderQuantity;
                        _batchService.UpdateBatchBrandBalance(batch.BatchId, batchBrandBalance, userId);
                        if (batchBrandBalance > 0)
                        {

                            makeDelivery = new MakeDelivery()
                            {
                                StockReduced = soldOut,
                                OrderQuantityBalance = orderBalance,
                                BatchBrandBalance = batchBrandBalance,

                            };
                            return makeDelivery;
                        }
                        else
                        {

                            makeDelivery = new MakeDelivery()
                            {
                                StockReduced = true,
                                OrderQuantityBalance = orderBalance,
                                BatchBrandBalance = batchBrandBalance,

                            };
                            return makeDelivery;
                        }
                    }



                }
                else
                {
                    //batch doesn't have enough quantities
                }
            }
            return makeDelivery;
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
                    Order order = null;
                    order = _orderService.GetOrder(delivery.OrderId);
                    if (orderBalance <= 0)
                    {
                        orderBalance = Convert.ToDouble(order.Balance);
                    }
                    else
                    {
                        delivery.Quantity = orderBalance;
                        
                    }
                    
                    if (batch.BrandBalance > 0)
                    {
                        
                       makeDelivery = ReduceBatchBrandStock(batch, orderBalance, delivery.Quantity, userId);
                        if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                        {
                            soldOutAmount = batch.BrandBalance - batchBrandBalance;
                            BatchDeliveryDetails batchDetails = new BatchDeliveryDetails()
                            {
                                BatchId = batch.BatchId,
                                BatchNumber = batch.Name,
                                BatchQuantity = soldOutAmount
                            };

                            batchDeliveryList.Add(batchDetails);
                            makeDelivery = new MakeDelivery()
                            {
                                StockReduced = true,
                                OrderQuantityBalance = 0,
                                BatchList = batchDeliveryList,
                            };
                            orderBalance = makeDelivery.OrderQuantityBalance;
                            _orderService.UpdateOrderWithBalance(order.OrderId, makeDelivery.OrderQuantityBalance, userId);

                            return makeDelivery;
                        }
                        else if (!makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                        {
                            soldOutAmount = batch.BrandBalance - batchBrandBalance;
                            BatchDeliveryDetails batchDetails = new BatchDeliveryDetails()
                            {
                                BatchId = batch.BatchId,
                                BatchNumber = batch.Name,
                                BatchQuantity = soldOutAmount
                            };

                            batchDeliveryList.Add(batchDetails);
                            makeDelivery = new MakeDelivery()
                            {
                                StockReduced = true,
                                OrderQuantityBalance = 0,
                                BatchList = batchDeliveryList,
                            };
                            _orderService.UpdateOrderWithBalance(order.OrderId, makeDelivery.OrderQuantityBalance, userId);
                            orderBalance = makeDelivery.OrderQuantityBalance;
                            return makeDelivery;
                        }

                        else if (!makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance > 0)
                        {
                            soldOutAmount = batch.BrandBalance - batchBrandBalance;
                            BatchDeliveryDetails batchDetails = new BatchDeliveryDetails()
                            {
                                BatchId = batch.BatchId,
                                BatchNumber = batch.Name,
                                BatchQuantity = soldOutAmount
                            };

                            batchDeliveryList.Add(batchDetails);
                            makeDelivery = new MakeDelivery()
                            {
                                StockReduced = false,
                                OrderQuantityBalance = 0,
                                BatchList = batchDeliveryList,
                            };
                            _orderService.UpdateOrderWithBalance(order.OrderId, makeDelivery.OrderQuantityBalance, userId);
                            orderBalance = makeDelivery.OrderQuantityBalance;
                            
                        }
                        else if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance > 0)
                        {
                            soldOutAmount = batch.BrandBalance - batchBrandBalance;
                            BatchDeliveryDetails batchDetails = new BatchDeliveryDetails()
                            {
                                BatchId = batch.BatchId,
                                BatchNumber = batch.Name,
                                BatchQuantity = soldOutAmount
                            };

                            batchDeliveryList.Add(batchDetails);
                            makeDelivery = new MakeDelivery()
                            {
                                StockReduced = true,
                                OrderQuantityBalance = makeDelivery.OrderQuantityBalance,
                                BatchList = batchDeliveryList,
                            };
                            _orderService.UpdateOrderWithBalance(order.OrderId, makeDelivery.OrderQuantityBalance, userId);
                            orderBalance = makeDelivery.OrderQuantityBalance;

                        }
                       
                    }
                   
                }
            }
            return makeDelivery;
        }
        #endregion
    #endregion


        public long SaveDelivery(Delivery delivery, string userId)
        {
            double totalQuantity = delivery.Quantity;
            long deliveryId = 0;
            MakeDelivery makeDelivery = new MakeDelivery();

            if (delivery.OrderId != 0)
            {
                #region deliver brand
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
                    makeDelivery = MakeBrandDeliveryRecord(delivery.StoreId, delivery, userId);
                    if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                    {
                        deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);

                        _orderService.UpdateOrderWithCompletedStatus(delivery.OrderId, orderStatusIdComplete, makeDelivery.OrderQuantityBalance, userId);

                        foreach (var deliveryBatch in makeDelivery.BatchList)
                        {
                            var batchDelivery = new DeliveryBatchDTO()
                            {
                                BatchId = deliveryBatch.BatchId,
                                BatchQuantity = deliveryBatch.BatchQuantity,
                                Price = delivery.Price,
                                DeliveryId = deliveryId,
                            };
                          this.SaveDeliveryBatch(batchDelivery);
                        }
                        #region generate documents
                        if (deliveryDTO.PaymentModeId == 2 || deliveryDTO.PaymentModeId == 1 || deliveryDTO.PaymentModeId == 10004)
                        {
                            //generate receipt
                            // throw new NotImplementedException();
                            var username = string.Empty;
                            var user = _userService.GetAspNetUser(delivery.CustomerId);
                            if (user != null)
                            {
                                username = user.FirstName + " " + user.LastName;
                            }
                            var document = new Document()
                            {
                                DocumentId = 0,

                                UserId = username,
                                DocumentCategoryId = receiptId,
                                Amount = delivery.Amount,
                                BranchId = delivery.BranchId,
                                ItemId = deliveryId,
                                Description = "Delivery of maize brand of " + totalQuantity.ToString() + " kgs" + "at" + delivery.Price + "Per Kilogram",



                            };

                            var documentId = _documentService.SaveDocument(document, userId);
                        }
                        else
                        {
                            //Generate  Invoice
                            // throw new NotImplementedException();
                            var username = string.Empty;
                            var user = _userService.GetAspNetUser(delivery.CustomerId);
                            if (user != null)
                            {
                                username = user.FirstName + " " + user.LastName;
                            }
                            var document = new Document()
                            {
                                DocumentId = 0,

                                UserId = username,
                                DocumentCategoryId = invoiceId,
                                Amount = delivery.Amount,
                                BranchId = delivery.BranchId,
                                ItemId = deliveryId,
                                Description = "Delivery of maize brand of " + totalQuantity.ToString() + " kgs" + "at" + delivery.Price + "Per Kilogram",



                            };

                            var documentId = _documentService.SaveDocument(document, userId);
                        }
                        #endregion



                    }
                    else if (!makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                    {
                        deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);

                        _orderService.UpdateOrderWithInProgressStatus(delivery.OrderId, orderStatusIdInProgress, makeDelivery.OrderQuantityBalance, userId);
                        foreach (var deliveryBatch in makeDelivery.BatchList)
                        {
                            var batchDelivery = new DeliveryBatchDTO()
                            {
                                BatchId = deliveryBatch.BatchId,
                                BatchQuantity = deliveryBatch.BatchQuantity,
                                Price = delivery.Price,
                                DeliveryId = deliveryId,
                            };
                            this.SaveDeliveryBatch(batchDelivery);
                        }

                        #region generate documents
                        if (deliveryDTO.PaymentModeId == 2 || deliveryDTO.PaymentModeId == 1 || deliveryDTO.PaymentModeId == 10004)
                        {
                            //generate receipt
                            // throw new NotImplementedException();
                            var username = string.Empty;
                            var user = _userService.GetAspNetUser(delivery.CustomerId);
                            if (user != null)
                            {
                                username = user.FirstName + " " + user.LastName;
                            }
                            var document = new Document()
                            {
                                DocumentId = 0,

                                UserId = username,
                                DocumentCategoryId = receiptId,
                                Amount = delivery.Amount,
                                BranchId = delivery.BranchId,
                                ItemId = deliveryId,
                                Description = "Delivery of maize brand of " + totalQuantity.ToString() + " kgs" + "at" + delivery.Price + "Per Kilogram",



                            };

                            var documentId = _documentService.SaveDocument(document, userId);
                        }
                        else
                        {
                            //Generate  Invoice
                            // throw new NotImplementedException();
                            var username = string.Empty;
                            var user = _userService.GetAspNetUser(delivery.CustomerId);
                            if (user != null)
                            {
                                username = user.FirstName + " " + user.LastName;
                            }
                            var document = new Document()
                            {
                                DocumentId = 0,

                                UserId = username,
                                DocumentCategoryId = invoiceId,
                                Amount = delivery.Amount,
                                BranchId = delivery.BranchId,
                                ItemId = deliveryId,
                                Description = "Delivery of maize brand of " + totalQuantity.ToString() + " kgs" + "at" + delivery.Price + "Per Kilogram",



                            };

                            var documentId = _documentService.SaveDocument(document, userId);
                        }
                        #endregion
                    }
                    else if (!makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance <= 0)
                    {
                        // no batches were selected
                        return -1;
                    }
                   

                    else if (!makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance > 0)
                    {
                        deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);

                        _orderService.UpdateOrderWithInProgressStatus(delivery.OrderId, orderStatusIdInProgress, makeDelivery.OrderQuantityBalance, userId);

                        foreach (var deliveryBatch in makeDelivery.BatchList)
                        {
                            var batchDelivery = new DeliveryBatchDTO()
                            {
                                BatchId = deliveryBatch.BatchId,
                                BatchQuantity = deliveryBatch.BatchQuantity,
                                Price = delivery.Price,
                                DeliveryId = deliveryId,
                            };
                            this.SaveDeliveryBatch(batchDelivery);
                        }
                        #region generate documents
                        if (deliveryDTO.PaymentModeId == 2 || deliveryDTO.PaymentModeId == 1 || deliveryDTO.PaymentModeId == 10004)
                        {
                            //generate receipt
                            // throw new NotImplementedException();
                            var username = string.Empty;
                            var user = _userService.GetAspNetUser(delivery.CustomerId);
                            if (user != null)
                            {
                                username = user.FirstName + " " + user.LastName;
                            }
                            var document = new Document()
                            {
                                DocumentId = 0,

                                UserId = username,
                                DocumentCategoryId = receiptId,
                                Amount = delivery.Amount,
                                BranchId = delivery.BranchId,
                                ItemId = deliveryId,
                                Description = "Delivery of maize brand of " + totalQuantity.ToString() + " kgs" + "at" + delivery.Price + "Per Kilogram",



                            };

                            var documentId = _documentService.SaveDocument(document, userId);
                        }
                        else
                        {
                            //Generate  Invoice
                            // throw new NotImplementedException();
                            var username = string.Empty;
                            var user = _userService.GetAspNetUser(delivery.CustomerId);
                            if (user != null)
                            {
                                username = user.FirstName + " " + user.LastName;
                            }
                            var document = new Document()
                            {
                                DocumentId = 0,

                                UserId = username,
                                DocumentCategoryId = invoiceId,
                                Amount = delivery.Amount,
                                BranchId = delivery.BranchId,
                                ItemId = deliveryId,
                                Description = "Delivery of maize brand of " + totalQuantity.ToString() + " kgs" + "at" + delivery.Price + "Per Kilogram",



                            };

                            var documentId = _documentService.SaveDocument(document, userId);
                        }
                        #endregion
                    }
                    else if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance > 0)
                    {
                        deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);

                        _orderService.UpdateOrderWithInProgressStatus(delivery.OrderId, orderStatusIdInProgress, makeDelivery.OrderQuantityBalance, userId);

                        foreach (var deliveryBatch in makeDelivery.BatchList)
                        {
                            var batchDelivery = new DeliveryBatchDTO()
                            {
                                BatchId = deliveryBatch.BatchId,
                                BatchQuantity = deliveryBatch.BatchQuantity,
                                Price = delivery.Price,
                                DeliveryId = deliveryId,
                            };
                            this.SaveDeliveryBatch(batchDelivery);
                        }
                        #region generate documents
                        if (deliveryDTO.PaymentModeId == 2 || deliveryDTO.PaymentModeId == 1 || deliveryDTO.PaymentModeId == 10004)
                        {
                            //generate receipt
                            // throw new NotImplementedException();
                            var username = string.Empty;
                            var user = _userService.GetAspNetUser(delivery.CustomerId);
                            if (user != null)
                            {
                                username = user.FirstName + " " + user.LastName;
                            }
                            var document = new Document()
                            {
                                DocumentId = 0,

                                UserId = username,
                                DocumentCategoryId = receiptId,
                                Amount = delivery.Amount,
                                BranchId = delivery.BranchId,
                                ItemId = deliveryId,
                                Description = "Delivery of maize brand of " + totalQuantity.ToString() + " kgs" + "at" + delivery.Price + "Per Kilogram",



                            };

                            var documentId = _documentService.SaveDocument(document, userId);
                        }
                        else
                        {
                            //Generate  Invoice
                            // throw new NotImplementedException();
                            var username = string.Empty;
                            var user = _userService.GetAspNetUser(delivery.CustomerId);
                            if (user != null)
                            {
                                username = user.FirstName + " " + user.LastName;
                            }
                            var document = new Document()
                            {
                                DocumentId = 0,

                                UserId = username,
                                DocumentCategoryId = invoiceId,
                                Amount = delivery.Amount,
                                BranchId = delivery.BranchId,
                                ItemId = deliveryId,
                                Description = "Delivery of maize brand of " + totalQuantity.ToString() + " kgs" + "at" + delivery.Price + "Per Kilogram",



                            };

                            var documentId = _documentService.SaveDocument(document, userId);
                        }
                        #endregion
                    }
                }
                #endregion

                #region deliver flour
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
                        makeDelivery = MakeFlourDeliveryRecord(delivery, userId);
                        if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance == 0)
                        {
                            deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);
                            if (delivery.Batches != null)
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

                            _orderService.UpdateOrderWithCompletedStatus(delivery.OrderId, orderStatusIdComplete, makeDelivery.OrderQuantityBalance, userId);

                            
                            List<DeliveryGradeSize> deliveryGradeSizeList = new List<DeliveryGradeSize>();
                            if (delivery.SelectedDeliveryGrades.Any())
                            {
                                foreach (var grade in delivery.SelectedDeliveryGrades)
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
                            else{
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
                            #region generate documents
                            if (deliveryDTO.PaymentModeId == 2 || deliveryDTO.PaymentModeId == 1 || deliveryDTO.PaymentModeId == 10004)
                            {
                                //generate receipt
                                //throw new NotImplementedException();
                                var username = string.Empty;
                                var user = _userService.GetAspNetUser(delivery.CustomerId);
                                if (user != null)
                                {
                                    username = user.FirstName + " " + user.LastName;
                                }
                                var document = new Document()
                                {
                                    DocumentId = 0,

                                    UserId = username,
                                    DocumentCategoryId = receiptId,
                                    Amount = delivery.Amount,
                                    BranchId = delivery.BranchId,
                                    ItemId = deliveryId,
                                    Description = "Delivery of maize flour of " + delivery.Quantity.ToString() + " kgs",



                                };

                                var documentId = _documentService.SaveDocument(document, userId);
                            }
                            else
                            {
                                //Generate  Invoice
                                //throw new NotImplementedException();
                                var username = string.Empty;
                                var user = _userService.GetAspNetUser(delivery.CustomerId);
                                if (user != null)
                                {
                                    username = user.FirstName + " " + user.LastName;
                                }
                                var document = new Document()
                                {
                                    DocumentId = 0,

                                    UserId = username,
                                    DocumentCategoryId = invoiceId,
                                    Amount = delivery.Amount,
                                    BranchId = delivery.BranchId,
                                    ItemId = deliveryId,
                                    Description = "Delivery of maize flour of " + delivery.Quantity.ToString() + " kgs",



                                };

                                var documentId = _documentService.SaveDocument(document, userId);
                            }
                            #endregion


                        }
                        else if (makeDelivery.StockReduced && makeDelivery.OrderQuantityBalance > 0)
                        {
                            deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);
                            if (delivery.Batches != null)
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

                            _orderService.UpdateOrderWithInProgressStatus(delivery.OrderId, orderStatusIdInProgress, makeDelivery.OrderQuantityBalance, userId);


                            List<DeliveryGradeSize> deliveryGradeSizeList = new List<DeliveryGradeSize>();

                            if (delivery.SelectedDeliveryGrades.Any())
                            {
                                foreach (var grade in delivery.SelectedDeliveryGrades)
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
                            else
                            {
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
                            #region generate documents
                            if (deliveryDTO.PaymentModeId == 2 || deliveryDTO.PaymentModeId == 1 || deliveryDTO.PaymentModeId == 10004)
                            {
                                //generate receipt
                                //throw new NotImplementedException();
                                var username = string.Empty;
                                var user = _userService.GetAspNetUser(delivery.CustomerId);
                                if (user != null)
                                {
                                    username = user.FirstName + " " + user.LastName;
                                }
                                var document = new Document()
                                {
                                    DocumentId = 0,

                                    UserId = username,
                                    DocumentCategoryId = receiptId,
                                    Amount = delivery.Amount,
                                    BranchId = delivery.BranchId,
                                    ItemId = deliveryId,
                                    Description = "Delivery of maize flour of " + delivery.Quantity.ToString() + " kgs",



                                };

                                var documentId = _documentService.SaveDocument(document, userId);
                            }
                            else
                            {
                                //Generate  Invoice
                                //throw new NotImplementedException();
                                var username = string.Empty;
                                var user = _userService.GetAspNetUser(delivery.CustomerId);
                                if (user != null)
                                {
                                    username = user.FirstName + " " + user.LastName;
                                }
                                var document = new Document()
                                {
                                    DocumentId = 0,

                                    UserId = username,
                                    DocumentCategoryId = invoiceId,
                                    Amount = delivery.Amount,
                                    BranchId = delivery.BranchId,
                                    ItemId = deliveryId,
                                    Description = "Delivery of maize flour of " + delivery.Quantity.ToString() + " kgs",



                                };

                                var documentId = _documentService.SaveDocument(document, userId);
                            }
                            #endregion

                        }

                #endregion

             }
              
                    #region transactions

                    long transactionSubTypeId = 0;
                    var notes = string.Empty;
                    if (delivery.ProductId == 1)
                    {
                        transactionSubTypeId = flourTransactionSubTypeId;
                        notes = "Maize Flour Sale";
                    }
                    else if (delivery.ProductId == 2)
                    {
                        transactionSubTypeId = branTransactionSubTypeId;
                        notes = "Bran Sale of " + totalQuantity + "kgs at" + delivery.Price + "Per Kilogram";
                    }
                    var paymentMode = _accountTransactionActivityService.GetPaymentMode(delivery.PaymentModeId);
                    var paymentModeName = paymentMode.Name;
                    if (paymentModeName == "Credit" || paymentModeName=="AdvancePayment")
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
                        _cashService.SaveCash(cash, userId);


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
                    #endregion
                
               

            
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

    public    void SaveDeliveryBatchList(List<DeliveryBatch> deliveryBatchList)
        {
            if (deliveryBatchList != null)
            {
                if (deliveryBatchList.Any())
                {
                    foreach (var deliveryBatch in deliveryBatchList)
                    {
                        var deliveryBatchDTO = new DTO.DeliveryBatchDTO()
                        {
                            DeliveryId = deliveryBatch.DeliveryId,
                            BatchQuantity = deliveryBatch.BatchQuantity,
                            BatchId  = deliveryBatch.BatchId,
                            Price = deliveryBatch.Price,
                             CreatedOn = deliveryBatch.CreatedOn,
                            TimeStamp = deliveryBatch.TimeStamp
                        };
                        this.SaveDeliveryBatch(deliveryBatchDTO);
                    }
                }
            }
        }

         public void SaveDeliveryBatch(DeliveryBatchDTO deliveryBatchDTO)
            {
                 _dataService.SaveDeliveryBatch(deliveryBatchDTO);
            }

        public  void SaveDeliveryGradeSizeList(List<DeliveryGradeSize> deliveryGradeSizeList)
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
        public  void SaveDeliveryGradeSize(DeliveryGradeSizeDTO deliveryGradeSizeDTO)
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
            long documentId = 0;
            if (data != null)
            {
                var document = _documentService.GetDocumentForAParticularItem(data.DeliveryId);
                if (document != null)
                {
                    documentId = document.DocumentId;
                }
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
                   DocumentId = documentId,
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
                                         //delivery.Quantity += (ogs.Quantity * ogs.Size.Value);
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
