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
    
    public partial class Order
    {
        public Order()
        {
            this.OrderGradeSizes = new HashSet<OrderGradeSize>();
            this.Deliveries = new HashSet<Delivery>();
        }
    
        public long OrderId { get; set; }
        public Nullable<double> Amount { get; set; }
        public long StatusId { get; set; }
        public string CustomerId { get; set; }
        public long ProductId { get; set; }
        public long BranchId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual ICollection<OrderGradeSize> OrderGradeSizes { get; set; }
        public virtual Product Product { get; set; }
        public virtual Status Status { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual AspNetUser AspNetUser2 { get; set; }
        public virtual AspNetUser AspNetUser3 { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
