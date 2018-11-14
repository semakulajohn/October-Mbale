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
    
    public partial class Batch
    {
        public Batch()
        {
            this.ActivityBatchCasuals = new HashSet<ActivityBatchCasual>();
            this.CasualActivities = new HashSet<CasualActivity>();
            this.BatchProducts = new HashSet<BatchProduct>();
            this.Stocks = new HashSet<Stock>();
            this.Deliveries = new HashSet<Delivery>();
            this.BatchSupplies = new HashSet<BatchSupply>();
            this.BatchOutPuts = new HashSet<BatchOutPut>();
            this.FactoryExpenses = new HashSet<FactoryExpense>();
            this.LabourCosts = new HashSet<LabourCost>();
            this.MachineRepairs = new HashSet<MachineRepair>();
            this.OtherExpenses = new HashSet<OtherExpense>();
            this.Utilities = new HashSet<Utility>();
        }
    
        public long BatchId { get; set; }
        public long SectorId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public long BranchId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    
        public virtual ICollection<ActivityBatchCasual> ActivityBatchCasuals { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual ICollection<CasualActivity> CasualActivities { get; set; }
        public virtual ICollection<BatchProduct> BatchProducts { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<BatchSupply> BatchSupplies { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual ICollection<BatchOutPut> BatchOutPuts { get; set; }
        public virtual ICollection<FactoryExpense> FactoryExpenses { get; set; }
        public virtual ICollection<LabourCost> LabourCosts { get; set; }
        public virtual ICollection<MachineRepair> MachineRepairs { get; set; }
        public virtual ICollection<OtherExpense> OtherExpenses { get; set; }
        public virtual ICollection<Utility> Utilities { get; set; }
    }
}
