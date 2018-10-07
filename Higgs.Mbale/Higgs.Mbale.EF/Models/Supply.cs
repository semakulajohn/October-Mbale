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
    
    public partial class Supply
    {
        public Supply()
        {
            this.BatchSupplies = new HashSet<BatchSupply>();
            this.StoreMaizeStocks = new HashSet<StoreMaizeStock>();
        }
    
        public long SupplyId { get; set; }
        public long BranchId { get; set; }
        public double Amount { get; set; }
        public string TruckNumber { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string SupplierId { get; set; }
        public System.DateTime SupplyDate { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public bool Used { get; set; }
        public string WeightNoteNumber { get; set; }
        public Nullable<double> MoistureContent { get; set; }
        public double BagsOfStones { get; set; }
        public double NormalBags { get; set; }
        public bool IsPaid { get; set; }
        public long StatusId { get; set; }
        public long StoreId { get; set; }
        public string Offloading { get; set; }
        public double AmountToPay { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual AspNetUser AspNetUser2 { get; set; }
        public virtual AspNetUser AspNetUser3 { get; set; }
        public virtual ICollection<BatchSupply> BatchSupplies { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Status Status { get; set; }
        public virtual Supply Supply1 { get; set; }
        public virtual Supply Supply2 { get; set; }
        public virtual ICollection<StoreMaizeStock> StoreMaizeStocks { get; set; }
        public virtual Store Store { get; set; }
    }
}