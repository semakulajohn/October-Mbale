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
    
    public partial class Size
    {
        public Size()
        {
            this.OrderGradeSizes = new HashSet<OrderGradeSize>();
            this.StockGradeSizes = new HashSet<StockGradeSize>();
            this.BatchGradeSizes = new HashSet<BatchGradeSize>();
            this.StoreGradeSizes = new HashSet<StoreGradeSize>();
            this.BuveraGradeSizes = new HashSet<BuveraGradeSize>();
            this.StoreBuveraGradeSizes = new HashSet<StoreBuveraGradeSize>();
            this.DeliveryGradeSizes = new HashSet<DeliveryGradeSize>();
            this.FlourTransferGradeSizes = new HashSet<FlourTransferGradeSize>();
            this.BuveraTransferGradeSizes = new HashSet<BuveraTransferGradeSize>();
            this.StoreBuveraTransferGradeSizes = new HashSet<StoreBuveraTransferGradeSize>();
            this.StoreFlourTransferGradeSizes = new HashSet<StoreFlourTransferGradeSize>();
        }
    
        public long SizeId { get; set; }
        public int Value { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<double> Rate { get; set; }
    
        public virtual ICollection<OrderGradeSize> OrderGradeSizes { get; set; }
        public virtual ICollection<StockGradeSize> StockGradeSizes { get; set; }
        public virtual ICollection<BatchGradeSize> BatchGradeSizes { get; set; }
        public virtual ICollection<StoreGradeSize> StoreGradeSizes { get; set; }
        public virtual ICollection<BuveraGradeSize> BuveraGradeSizes { get; set; }
        public virtual ICollection<StoreBuveraGradeSize> StoreBuveraGradeSizes { get; set; }
        public virtual ICollection<DeliveryGradeSize> DeliveryGradeSizes { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual AspNetUser AspNetUser2 { get; set; }
        public virtual ICollection<FlourTransferGradeSize> FlourTransferGradeSizes { get; set; }
        public virtual ICollection<BuveraTransferGradeSize> BuveraTransferGradeSizes { get; set; }
        public virtual ICollection<StoreBuveraTransferGradeSize> StoreBuveraTransferGradeSizes { get; set; }
        public virtual ICollection<StoreFlourTransferGradeSize> StoreFlourTransferGradeSizes { get; set; }
    }
}
