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
    
    public partial class Delivery
    {
        public Delivery()
        {
            this.DeliveryGradeSizes = new HashSet<DeliveryGradeSize>();
            this.DeliveryStocks = new HashSet<DeliveryStock>();
        }
    
        public long DeliveryId { get; set; }
        public string CustomerId { get; set; }
        public string DriverName { get; set; }
        public Nullable<double> Price { get; set; }
        public long BatchId { get; set; }
        public long ProductId { get; set; }
        public long PaymentModeId { get; set; }
        public double DeliveryCost { get; set; }
        public string DriverNIN { get; set; }
        public string VehicleNumber { get; set; }
        public long OrderId { get; set; }
        public long TransactionSubTypeId { get; set; }
        public long MediaId { get; set; }
        public long BranchId { get; set; }
        public long SectorId { get; set; }
        public double Amount { get; set; }
        public long StoreId { get; set; }
        public string Location { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public double Quantity { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual AspNetUser AspNetUser2 { get; set; }
        public virtual Batch Batch { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<DeliveryGradeSize> DeliveryGradeSizes { get; set; }
        public virtual ICollection<DeliveryStock> DeliveryStocks { get; set; }
        public virtual Order Order { get; set; }
        public virtual PaymentMode PaymentMode { get; set; }
        public virtual Product Product { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual Store Store { get; set; }
        public virtual TransactionSubType TransactionSubType { get; set; }
    }
}
