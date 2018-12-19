﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Higgs.Mbale.EF.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Higgs.Mbale.EF.Context;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MbaleEntities : DbContext,IDbContext
    {
        public MbaleEntities()
            : base("name=MbaleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<ExtensionType> ExtensionTypes { get; set; }
        public virtual DbSet<Factory> Factories { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<BatchProduct> BatchProducts { get; set; }
        public virtual DbSet<TransactionSubType> TransactionSubTypes { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<BranchSector> BranchSectors { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<UserBranchManager> UserBranchManagers { get; set; }
        public virtual DbSet<ActivityBatchCasual> ActivityBatchCasuals { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityBranch> ActivityBranches { get; set; }
        public virtual DbSet<OrderGradeSize> OrderGradeSizes { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<PaymentMode> PaymentModes { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockGradeSize> StockGradeSizes { get; set; }
        public virtual DbSet<CasualActivity> CasualActivities { get; set; }
        public virtual DbSet<StockProduct> StockProducts { get; set; }
        public virtual DbSet<UserBranch> UserBranches { get; set; }
        public virtual DbSet<StoreStock> StoreStocks { get; set; }
        public virtual DbSet<StoreMaizeStock> StoreMaizeStocks { get; set; }
        public virtual DbSet<StoreGradeSize> StoreGradeSizes { get; set; }
        public virtual DbSet<DeliveryStock> DeliveryStocks { get; set; }
        public virtual DbSet<BuveraGradeSize> BuveraGradeSizes { get; set; }
        public virtual DbSet<StoreBuveraGradeSize> StoreBuveraGradeSizes { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategories { get; set; }
        public virtual DbSet<DeliveryGradeSize> DeliveryGradeSizes { get; set; }
        public virtual DbSet<Creditor> Creditors { get; set; }
        public virtual DbSet<Debtor> Debtors { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }
        public virtual DbSet<AccountTransactionActivity> AccountTransactionActivities { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Cash> Cashes { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<BatchSupply> BatchSupplies { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<FlourTransferGradeSize> FlourTransferGradeSizes { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<BatchOutPut> BatchOutPuts { get; set; }
        public virtual DbSet<FactoryExpense> FactoryExpenses { get; set; }
        public virtual DbSet<LabourCost> LabourCosts { get; set; }
        public virtual DbSet<MachineRepair> MachineRepairs { get; set; }
        public virtual DbSet<OtherExpense> OtherExpenses { get; set; }
        public virtual DbSet<Utility> Utilities { get; set; }
        public virtual DbSet<Buvera> Buveras { get; set; }
        public virtual DbSet<BuveraTransfer> BuveraTransfers { get; set; }
        public virtual DbSet<BuveraTransferGradeSize> BuveraTransferGradeSizes { get; set; }
        public virtual DbSet<StoreBuveraTransferGradeSize> StoreBuveraTransferGradeSizes { get; set; }
        public virtual DbSet<StoreFlourTransferGradeSize> StoreFlourTransferGradeSizes { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<DeliveryBatch> DeliveryBatches { get; set; }
        public virtual DbSet<FlourTransfer> FlourTransfers { get; set; }
        public virtual DbSet<FlourTransferBatch> FlourTransferBatches { get; set; }
        public virtual DbSet<Requistion> Requistions { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<CasualWorker> CasualWorkers { get; set; }
        public virtual DbSet<BatchGradeSize> BatchGradeSizes { get; set; }
    
        public virtual int Mark_FactoryExpense_AsDeleted(Nullable<long> inPutFactoryExpenseId, string userId)
        {
            var inPutFactoryExpenseIdParameter = inPutFactoryExpenseId.HasValue ?
                new ObjectParameter("inPutFactoryExpenseId", inPutFactoryExpenseId) :
                new ObjectParameter("inPutFactoryExpenseId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Mark_FactoryExpense_AsDeleted", inPutFactoryExpenseIdParameter, userIdParameter);
        }
    
        public virtual int Mark_LabourCost_AsDeleted(Nullable<long> inPutLabourCostId, string userId)
        {
            var inPutLabourCostIdParameter = inPutLabourCostId.HasValue ?
                new ObjectParameter("inPutLabourCostId", inPutLabourCostId) :
                new ObjectParameter("inPutLabourCostId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Mark_LabourCost_AsDeleted", inPutLabourCostIdParameter, userIdParameter);
        }
    
        public virtual int Mark_MachineRepair_AsDeleted(Nullable<long> inPutRepairId, string userId)
        {
            var inPutRepairIdParameter = inPutRepairId.HasValue ?
                new ObjectParameter("inPutRepairId", inPutRepairId) :
                new ObjectParameter("inPutRepairId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Mark_MachineRepair_AsDeleted", inPutRepairIdParameter, userIdParameter);
        }
    
        public virtual int Mark_CasualActivity_AsDeleted(Nullable<long> inPutCasualActivityId, string userId)
        {
            var inPutCasualActivityIdParameter = inPutCasualActivityId.HasValue ?
                new ObjectParameter("inPutCasualActivityId", inPutCasualActivityId) :
                new ObjectParameter("inPutCasualActivityId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Mark_CasualActivity_AsDeleted", inPutCasualActivityIdParameter, userIdParameter);
        }
    
        public virtual int Mark_OtherExpense_AsDeleted(Nullable<long> inPutOtherExpenseId, string userId)
        {
            var inPutOtherExpenseIdParameter = inPutOtherExpenseId.HasValue ?
                new ObjectParameter("inPutOtherExpenseId", inPutOtherExpenseId) :
                new ObjectParameter("inPutOtherExpenseId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Mark_OtherExpense_AsDeleted", inPutOtherExpenseIdParameter, userIdParameter);
        }
    
        public virtual int UpdateSupplyWithCompletedStatus(Nullable<long> inPutSupplyId, Nullable<long> statusId, string userId)
        {
            var inPutSupplyIdParameter = inPutSupplyId.HasValue ?
                new ObjectParameter("inPutSupplyId", inPutSupplyId) :
                new ObjectParameter("inPutSupplyId", typeof(long));
    
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("statusId", statusId) :
                new ObjectParameter("statusId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateSupplyWithCompletedStatus", inPutSupplyIdParameter, statusIdParameter, userIdParameter);
        }
    
        public virtual int Mark_Activity_AsDeleted(Nullable<long> inPutActivityId, string userId)
        {
            var inPutActivityIdParameter = inPutActivityId.HasValue ?
                new ObjectParameter("inPutActivityId", inPutActivityId) :
                new ObjectParameter("inPutActivityId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Mark_Activity_AsDeleted", inPutActivityIdParameter, userIdParameter);
        }
    
        public virtual int UpdateStoreStockWithSoldOut(Nullable<long> inPutStockId, Nullable<bool> soldOut, Nullable<long> inPutProductId, string userId)
        {
            var inPutStockIdParameter = inPutStockId.HasValue ?
                new ObjectParameter("inPutStockId", inPutStockId) :
                new ObjectParameter("inPutStockId", typeof(long));
    
            var soldOutParameter = soldOut.HasValue ?
                new ObjectParameter("soldOut", soldOut) :
                new ObjectParameter("soldOut", typeof(bool));
    
            var inPutProductIdParameter = inPutProductId.HasValue ?
                new ObjectParameter("inPutProductId", inPutProductId) :
                new ObjectParameter("inPutProductId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateStoreStockWithSoldOut", inPutStockIdParameter, soldOutParameter, inPutProductIdParameter, userIdParameter);
        }
    
        public virtual int UpdateOrderWithCompletedStatus(Nullable<long> inPutOrderId, Nullable<long> statusId, Nullable<double> balance, string userId)
        {
            var inPutOrderIdParameter = inPutOrderId.HasValue ?
                new ObjectParameter("inPutOrderId", inPutOrderId) :
                new ObjectParameter("inPutOrderId", typeof(long));
    
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("statusId", statusId) :
                new ObjectParameter("statusId", typeof(long));
    
            var balanceParameter = balance.HasValue ?
                new ObjectParameter("balance", balance) :
                new ObjectParameter("balance", typeof(double));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderWithCompletedStatus", inPutOrderIdParameter, statusIdParameter, balanceParameter, userIdParameter);
        }
    
        public virtual int Mark_Utility_AsDeleted(Nullable<long> inPutUtilityId, string userId)
        {
            var inPutUtilityIdParameter = inPutUtilityId.HasValue ?
                new ObjectParameter("inPutUtilityId", inPutUtilityId) :
                new ObjectParameter("inPutUtilityId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Mark_Utility_AsDeleted", inPutUtilityIdParameter, userIdParameter);
        }
    
        public virtual int Mark_Requistion_AsDeleted(Nullable<long> inPutRequistionId, string userId)
        {
            var inPutRequistionIdParameter = inPutRequistionId.HasValue ?
                new ObjectParameter("inPutRequistionId", inPutRequistionId) :
                new ObjectParameter("inPutRequistionId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Mark_Requistion_AsDeleted", inPutRequistionIdParameter, userIdParameter);
        }
    
        public virtual int UpdateRequistionWithCompletedStatus(Nullable<long> inPutRequistionId, Nullable<long> statusId, string userId)
        {
            var inPutRequistionIdParameter = inPutRequistionId.HasValue ?
                new ObjectParameter("inPutRequistionId", inPutRequistionId) :
                new ObjectParameter("inPutRequistionId", typeof(long));
    
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("statusId", statusId) :
                new ObjectParameter("statusId", typeof(long));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateRequistionWithCompletedStatus", inPutRequistionIdParameter, statusIdParameter, userIdParameter);
        }
    
        public virtual int UpdateOrderWithInProgressStatus(Nullable<long> inPutOrderId, Nullable<long> statusId, Nullable<double> balance, string userId)
        {
            var inPutOrderIdParameter = inPutOrderId.HasValue ?
                new ObjectParameter("inPutOrderId", inPutOrderId) :
                new ObjectParameter("inPutOrderId", typeof(long));
    
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("statusId", statusId) :
                new ObjectParameter("statusId", typeof(long));
    
            var balanceParameter = balance.HasValue ?
                new ObjectParameter("balance", balance) :
                new ObjectParameter("balance", typeof(double));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderWithInProgressStatus", inPutOrderIdParameter, statusIdParameter, balanceParameter, userIdParameter);
        }
    
        public virtual int UpdateBatchBrandQuantity(Nullable<long> inPutBatchId, Nullable<double> quantity, string userId)
        {
            var inPutBatchIdParameter = inPutBatchId.HasValue ?
                new ObjectParameter("inPutBatchId", inPutBatchId) :
                new ObjectParameter("inPutBatchId", typeof(long));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("quantity", quantity) :
                new ObjectParameter("quantity", typeof(double));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateBatchBrandQuantity", inPutBatchIdParameter, quantityParameter, userIdParameter);
        }
    
        public virtual int UpdateOrderWithBalanceQuantity(Nullable<long> inPutOrderId, Nullable<double> quantity, string userId)
        {
            var inPutOrderIdParameter = inPutOrderId.HasValue ?
                new ObjectParameter("inPutOrderId", inPutOrderId) :
                new ObjectParameter("inPutOrderId", typeof(long));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("quantity", quantity) :
                new ObjectParameter("quantity", typeof(double));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderWithBalanceQuantity", inPutOrderIdParameter, quantityParameter, userIdParameter);
        }
    
        public virtual int UpdateOrderGradeSizes(Nullable<long> orderId, Nullable<long> gradeId, Nullable<long> sizeId, Nullable<double> quantity, Nullable<double> balance)
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("orderId", orderId) :
                new ObjectParameter("orderId", typeof(long));
    
            var gradeIdParameter = gradeId.HasValue ?
                new ObjectParameter("gradeId", gradeId) :
                new ObjectParameter("gradeId", typeof(long));
    
            var sizeIdParameter = sizeId.HasValue ?
                new ObjectParameter("sizeId", sizeId) :
                new ObjectParameter("sizeId", typeof(long));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("quantity", quantity) :
                new ObjectParameter("quantity", typeof(double));
    
            var balanceParameter = balance.HasValue ?
                new ObjectParameter("balance", balance) :
                new ObjectParameter("balance", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateOrderGradeSizes", orderIdParameter, gradeIdParameter, sizeIdParameter, quantityParameter, balanceParameter);
        }
    }
}
