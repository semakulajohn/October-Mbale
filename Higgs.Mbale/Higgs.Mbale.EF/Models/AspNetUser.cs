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
    
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogin>();
            this.AspNetUsers1 = new HashSet<AspNetUser>();
            this.AspNetUsers11 = new HashSet<AspNetUser>();
            this.AspNetUsers12 = new HashSet<AspNetUser>();
            this.Branches = new HashSet<Branch>();
            this.Branches1 = new HashSet<Branch>();
            this.Branches2 = new HashSet<Branch>();
            this.CasualWorkers = new HashSet<CasualWorker>();
            this.CasualWorkers1 = new HashSet<CasualWorker>();
            this.CasualWorkers2 = new HashSet<CasualWorker>();
            this.Factories = new HashSet<Factory>();
            this.Factories1 = new HashSet<Factory>();
            this.Factories2 = new HashSet<Factory>();
            this.Products = new HashSet<Product>();
            this.Products1 = new HashSet<Product>();
            this.Products2 = new HashSet<Product>();
            this.Sizes = new HashSet<Size>();
            this.Sizes1 = new HashSet<Size>();
            this.Sizes2 = new HashSet<Size>();
            this.Status = new HashSet<Status>();
            this.Status1 = new HashSet<Status>();
            this.Status2 = new HashSet<Status>();
            this.Stores = new HashSet<Store>();
            this.Stores1 = new HashSet<Store>();
            this.Stores2 = new HashSet<Store>();
            this.AspNetRoles = new HashSet<AspNetRole>();
            this.TransactionSubTypes = new HashSet<TransactionSubType>();
            this.TransactionSubTypes1 = new HashSet<TransactionSubType>();
            this.TransactionSubTypes2 = new HashSet<TransactionSubType>();
            this.Sectors = new HashSet<Sector>();
            this.Sectors1 = new HashSet<Sector>();
            this.Sectors2 = new HashSet<Sector>();
            this.Grades = new HashSet<Grade>();
            this.Grades1 = new HashSet<Grade>();
            this.Grades2 = new HashSet<Grade>();
            this.UserBranchManagers = new HashSet<UserBranchManager>();
            this.ActivityBatchCasuals = new HashSet<ActivityBatchCasual>();
            this.ActivityBatchCasuals1 = new HashSet<ActivityBatchCasual>();
            this.ActivityBatchCasuals2 = new HashSet<ActivityBatchCasual>();
            this.Activities = new HashSet<Activity>();
            this.Activities1 = new HashSet<Activity>();
            this.Activities2 = new HashSet<Activity>();
            this.Supplies = new HashSet<Supply>();
            this.Supplies1 = new HashSet<Supply>();
            this.Supplies2 = new HashSet<Supply>();
            this.Supplies3 = new HashSet<Supply>();
            this.FactoryExpenses = new HashSet<FactoryExpense>();
            this.FactoryExpenses1 = new HashSet<FactoryExpense>();
            this.FactoryExpenses2 = new HashSet<FactoryExpense>();
            this.MachineRepairs = new HashSet<MachineRepair>();
            this.MachineRepairs1 = new HashSet<MachineRepair>();
            this.MachineRepairs2 = new HashSet<MachineRepair>();
            this.LabourCosts = new HashSet<LabourCost>();
            this.LabourCosts1 = new HashSet<LabourCost>();
            this.LabourCosts2 = new HashSet<LabourCost>();
            this.PaymentModes = new HashSet<PaymentMode>();
            this.PaymentModes1 = new HashSet<PaymentMode>();
            this.Stocks = new HashSet<Stock>();
            this.Stocks1 = new HashSet<Stock>();
            this.Stocks2 = new HashSet<Stock>();
            this.CasualActivities = new HashSet<CasualActivity>();
            this.CasualActivities1 = new HashSet<CasualActivity>();
            this.CasualActivities2 = new HashSet<CasualActivity>();
            this.OtherExpenses = new HashSet<OtherExpense>();
            this.OtherExpenses1 = new HashSet<OtherExpense>();
            this.OtherExpenses2 = new HashSet<OtherExpense>();
            this.Batches = new HashSet<Batch>();
            this.Batches1 = new HashSet<Batch>();
            this.BatchOutPuts = new HashSet<BatchOutPut>();
            this.BatchOutPuts1 = new HashSet<BatchOutPut>();
            this.BatchOutPuts2 = new HashSet<BatchOutPut>();
            this.UserBranches = new HashSet<UserBranch>();
            this.Orders = new HashSet<Order>();
            this.Orders1 = new HashSet<Order>();
            this.Orders2 = new HashSet<Order>();
            this.Orders3 = new HashSet<Order>();
            this.Buveras = new HashSet<Buvera>();
            this.Buveras1 = new HashSet<Buvera>();
            this.Buveras2 = new HashSet<Buvera>();
            this.Utilities = new HashSet<Utility>();
            this.Utilities1 = new HashSet<Utility>();
            this.Utilities2 = new HashSet<Utility>();
            this.Inventories = new HashSet<Inventory>();
            this.Inventories1 = new HashSet<Inventory>();
            this.Inventories2 = new HashSet<Inventory>();
            this.Requistions = new HashSet<Requistion>();
            this.Requistions1 = new HashSet<Requistion>();
            this.Requistions2 = new HashSet<Requistion>();
            this.Requistions3 = new HashSet<Requistion>();
            this.Deliveries = new HashSet<Delivery>();
            this.Deliveries1 = new HashSet<Delivery>();
            this.Deliveries2 = new HashSet<Delivery>();
            this.Creditors = new HashSet<Creditor>();
            this.Creditors1 = new HashSet<Creditor>();
            this.Creditors2 = new HashSet<Creditor>();
            this.Creditors3 = new HashSet<Creditor>();
            this.Debtors = new HashSet<Debtor>();
            this.Debtors1 = new HashSet<Debtor>();
            this.Debtors2 = new HashSet<Debtor>();
            this.Debtors3 = new HashSet<Debtor>();
            this.AccountTransactionActivities = new HashSet<AccountTransactionActivity>();
            this.AccountTransactionActivities1 = new HashSet<AccountTransactionActivity>();
            this.AccountTransactionActivities2 = new HashSet<AccountTransactionActivity>();
            this.Documents = new HashSet<Document>();
            this.Documents1 = new HashSet<Document>();
            this.Documents2 = new HashSet<Document>();
            this.Documents3 = new HashSet<Document>();
            this.Transactions = new HashSet<Transaction>();
            this.Transactions1 = new HashSet<Transaction>();
            this.Transactions2 = new HashSet<Transaction>();
            this.Cashes = new HashSet<Cash>();
            this.Cashes1 = new HashSet<Cash>();
        }
    
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<int> GenderId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public string UniqueNumber { get; set; }
    
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers1 { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers11 { get; set; }
        public virtual AspNetUser AspNetUser2 { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers12 { get; set; }
        public virtual AspNetUser AspNetUser3 { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Branch> Branches1 { get; set; }
        public virtual ICollection<Branch> Branches2 { get; set; }
        public virtual ICollection<CasualWorker> CasualWorkers { get; set; }
        public virtual ICollection<CasualWorker> CasualWorkers1 { get; set; }
        public virtual ICollection<CasualWorker> CasualWorkers2 { get; set; }
        public virtual ICollection<Factory> Factories { get; set; }
        public virtual ICollection<Factory> Factories1 { get; set; }
        public virtual ICollection<Factory> Factories2 { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Product> Products1 { get; set; }
        public virtual ICollection<Product> Products2 { get; set; }
        public virtual ICollection<Size> Sizes { get; set; }
        public virtual ICollection<Size> Sizes1 { get; set; }
        public virtual ICollection<Size> Sizes2 { get; set; }
        public virtual ICollection<Status> Status { get; set; }
        public virtual ICollection<Status> Status1 { get; set; }
        public virtual ICollection<Status> Status2 { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<Store> Stores1 { get; set; }
        public virtual ICollection<Store> Stores2 { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
        public virtual ICollection<TransactionSubType> TransactionSubTypes { get; set; }
        public virtual ICollection<TransactionSubType> TransactionSubTypes1 { get; set; }
        public virtual ICollection<TransactionSubType> TransactionSubTypes2 { get; set; }
        public virtual ICollection<Sector> Sectors { get; set; }
        public virtual ICollection<Sector> Sectors1 { get; set; }
        public virtual ICollection<Sector> Sectors2 { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Grade> Grades1 { get; set; }
        public virtual ICollection<Grade> Grades2 { get; set; }
        public virtual ICollection<UserBranchManager> UserBranchManagers { get; set; }
        public virtual ICollection<ActivityBatchCasual> ActivityBatchCasuals { get; set; }
        public virtual ICollection<ActivityBatchCasual> ActivityBatchCasuals1 { get; set; }
        public virtual ICollection<ActivityBatchCasual> ActivityBatchCasuals2 { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Activity> Activities1 { get; set; }
        public virtual ICollection<Activity> Activities2 { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
        public virtual ICollection<Supply> Supplies1 { get; set; }
        public virtual ICollection<Supply> Supplies2 { get; set; }
        public virtual ICollection<Supply> Supplies3 { get; set; }
        public virtual ICollection<FactoryExpense> FactoryExpenses { get; set; }
        public virtual ICollection<FactoryExpense> FactoryExpenses1 { get; set; }
        public virtual ICollection<FactoryExpense> FactoryExpenses2 { get; set; }
        public virtual ICollection<MachineRepair> MachineRepairs { get; set; }
        public virtual ICollection<MachineRepair> MachineRepairs1 { get; set; }
        public virtual ICollection<MachineRepair> MachineRepairs2 { get; set; }
        public virtual ICollection<LabourCost> LabourCosts { get; set; }
        public virtual ICollection<LabourCost> LabourCosts1 { get; set; }
        public virtual ICollection<LabourCost> LabourCosts2 { get; set; }
        public virtual ICollection<PaymentMode> PaymentModes { get; set; }
        public virtual ICollection<PaymentMode> PaymentModes1 { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Stock> Stocks1 { get; set; }
        public virtual ICollection<Stock> Stocks2 { get; set; }
        public virtual ICollection<CasualActivity> CasualActivities { get; set; }
        public virtual ICollection<CasualActivity> CasualActivities1 { get; set; }
        public virtual ICollection<CasualActivity> CasualActivities2 { get; set; }
        public virtual ICollection<OtherExpense> OtherExpenses { get; set; }
        public virtual ICollection<OtherExpense> OtherExpenses1 { get; set; }
        public virtual ICollection<OtherExpense> OtherExpenses2 { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Batch> Batches1 { get; set; }
        public virtual ICollection<BatchOutPut> BatchOutPuts { get; set; }
        public virtual ICollection<BatchOutPut> BatchOutPuts1 { get; set; }
        public virtual ICollection<BatchOutPut> BatchOutPuts2 { get; set; }
        public virtual ICollection<UserBranch> UserBranches { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Order> Orders1 { get; set; }
        public virtual ICollection<Order> Orders2 { get; set; }
        public virtual ICollection<Order> Orders3 { get; set; }
        public virtual ICollection<Buvera> Buveras { get; set; }
        public virtual ICollection<Buvera> Buveras1 { get; set; }
        public virtual ICollection<Buvera> Buveras2 { get; set; }
        public virtual ICollection<Utility> Utilities { get; set; }
        public virtual ICollection<Utility> Utilities1 { get; set; }
        public virtual ICollection<Utility> Utilities2 { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Inventory> Inventories1 { get; set; }
        public virtual ICollection<Inventory> Inventories2 { get; set; }
        public virtual ICollection<Requistion> Requistions { get; set; }
        public virtual ICollection<Requistion> Requistions1 { get; set; }
        public virtual ICollection<Requistion> Requistions2 { get; set; }
        public virtual ICollection<Requistion> Requistions3 { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Delivery> Deliveries1 { get; set; }
        public virtual ICollection<Delivery> Deliveries2 { get; set; }
        public virtual ICollection<Creditor> Creditors { get; set; }
        public virtual ICollection<Creditor> Creditors1 { get; set; }
        public virtual ICollection<Creditor> Creditors2 { get; set; }
        public virtual ICollection<Creditor> Creditors3 { get; set; }
        public virtual ICollection<Debtor> Debtors { get; set; }
        public virtual ICollection<Debtor> Debtors1 { get; set; }
        public virtual ICollection<Debtor> Debtors2 { get; set; }
        public virtual ICollection<Debtor> Debtors3 { get; set; }
        public virtual ICollection<AccountTransactionActivity> AccountTransactionActivities { get; set; }
        public virtual ICollection<AccountTransactionActivity> AccountTransactionActivities1 { get; set; }
        public virtual ICollection<AccountTransactionActivity> AccountTransactionActivities2 { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Document> Documents1 { get; set; }
        public virtual ICollection<Document> Documents2 { get; set; }
        public virtual ICollection<Document> Documents3 { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Transaction> Transactions1 { get; set; }
        public virtual ICollection<Transaction> Transactions2 { get; set; }
        public virtual ICollection<Cash> Cashes { get; set; }
        public virtual ICollection<Cash> Cashes1 { get; set; }
    }
}
