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
    
    public partial class Creditor
    {
        public long CreditorId { get; set; }
        public string AspNetUserId { get; set; }
        public Nullable<long> CasualWorkerId { get; set; }
        public double Amount { get; set; }
        public bool Action { get; set; }
        public Nullable<long> BranchId { get; set; }
        public long SectorId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual Creditor Creditor1 { get; set; }
        public virtual Creditor Creditor2 { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual AspNetUser AspNetUser2 { get; set; }
        public virtual AspNetUser AspNetUser3 { get; set; }
        public virtual CasualWorker CasualWorker { get; set; }
    }
}
