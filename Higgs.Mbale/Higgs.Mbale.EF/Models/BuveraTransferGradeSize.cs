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
    
    public partial class BuveraTransferGradeSize
    {
        public long BuveraTransferId { get; set; }
        public long GradeId { get; set; }
        public long SizeId { get; set; }
        public long StoreId { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public System.DateTime TimeStamp { get; set; }
    
        public virtual BuveraTransfer BuveraTransfer { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual Size Size { get; set; }
        public virtual Store Store { get; set; }
    }
}
