//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Branch
    {
        public Branch()
        {
            this.Factories = new HashSet<Factory>();
            this.Stores = new HashSet<Store>();
            this.BranchSectors = new HashSet<BranchSector>();
            this.UserBranchManagers = new HashSet<UserBranchManager>();
            this.ActivityBranches = new HashSet<ActivityBranch>();
            this.Supplies = new HashSet<Supply>();
            this.Stocks = new HashSet<Stock>();
            this.CasualActivities = new HashSet<CasualActivity>();
            this.UserBranches = new HashSet<UserBranch>();
            this.StoreStocks = new HashSet<StoreStock>();
            this.StoreMaizeStocks = new HashSet<StoreMaizeStock>();
            this.Inventories = new HashSet<Inventory>();
            this.Creditors = new HashSet<Creditor>();
            this.Debtors = new HashSet<Debtor>();
            this.AccountTransactionActivities = new HashSet<AccountTransactionActivity>();
            this.Transactions = new HashSet<Transaction>();
            this.Cashes = new HashSet<Cash>();
            this.Batches = new HashSet<Batch>();
            this.Orders = new HashSet<Order>();
            this.BatchOutPuts = new HashSet<BatchOutPut>();
            this.FactoryExpenses = new HashSet<FactoryExpense>();
            this.LabourCosts = new HashSet<LabourCost>();
            this.MachineRepairs = new HashSet<MachineRepair>();
            this.OtherExpenses = new HashSet<OtherExpense>();
            this.Utilities = new HashSet<Utility>();
            this.Buveras = new HashSet<Buvera>();
            this.BuveraTransfers = new HashSet<BuveraTransfer>();
            this.Deliveries = new HashSet<Delivery>();
            this.FlourTransfers = new HashSet<FlourTransfer>();
            this.Requistions = new HashSet<Requistion>();
            this.Documents = new HashSet<Document>();
            this.CasualWorkers = new HashSet<CasualWorker>();
            this.CashTransfers = new HashSet<CashTransfer>();
            this.CashTransfers1 = new HashSet<CashTransfer>();
        }
    
        public long BranchId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public double MillingChargeRate { get; set; }
    
        public virtual ICollection<Factory> Factories { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<BranchSector> BranchSectors { get; set; }
        public virtual ICollection<UserBranchManager> UserBranchManagers { get; set; }
        public virtual ICollection<ActivityBranch> ActivityBranches { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<CasualActivity> CasualActivities { get; set; }
        public virtual ICollection<UserBranch> UserBranches { get; set; }
        public virtual ICollection<StoreStock> StoreStocks { get; set; }
        public virtual ICollection<StoreMaizeStock> StoreMaizeStocks { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Creditor> Creditors { get; set; }
        public virtual ICollection<Debtor> Debtors { get; set; }
        public virtual ICollection<AccountTransactionActivity> AccountTransactionActivities { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Cash> Cashes { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual AspNetUser AspNetUser2 { get; set; }
        public virtual ICollection<BatchOutPut> BatchOutPuts { get; set; }
        public virtual ICollection<FactoryExpense> FactoryExpenses { get; set; }
        public virtual ICollection<LabourCost> LabourCosts { get; set; }
        public virtual ICollection<MachineRepair> MachineRepairs { get; set; }
        public virtual ICollection<OtherExpense> OtherExpenses { get; set; }
        public virtual ICollection<Utility> Utilities { get; set; }
        public virtual ICollection<Buvera> Buveras { get; set; }
        public virtual ICollection<BuveraTransfer> BuveraTransfers { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<FlourTransfer> FlourTransfers { get; set; }
        public virtual ICollection<Requistion> Requistions { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<CasualWorker> CasualWorkers { get; set; }
        public virtual ICollection<CashTransfer> CashTransfers { get; set; }
        public virtual ICollection<CashTransfer> CashTransfers1 { get; set; }
    }
}
