﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
  public  interface IStockService
    {
        IEnumerable<Stock> GetAllStocks();
        Stock GetStock(long stockId);
        long SaveStock(Stock stock, string userId);
        void MarkAsDeleted(long stockId, string userId);
        IEnumerable<Stock> GetAllStocksForAParticularBranch(long branchId);
        IEnumerable<StoreStock> GetStocksForAParticularStore(long storeId);
        StoreGrade GetStoreFlourStockForView(IEnumerable<Models.StoreGradeSize> list);
        StoreGrade GetStoreFlourStock(long storeId);
        StoreStock GetStockForAParticularStoreForDelivery(long storeId, long productId,long batchId);
        void UpdateStoreStockAndStockDetails(long stockId, long productId, bool soldOut, string userId);
        void SaveStoreStock(StoreStock storeStock, bool inOrOut);
        IEnumerable<StoreGradeSize> GetStoreGradeSizeForParticularGradeAtAStore(long gradeId, long storeId);
    }
}
