﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
 public   class Transaction
    {
        public long TransactionId { get; set; }
        public Nullable<long> BranchId { get; set; }
        public long SectorId { get; set; }
        public double Amount { get; set; }
        public long TransactionTypeId { get; set; }
        public long TransactionSubTypeId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<long> SupplyId { get; set; }

        public string TransactionTypeName { get; set; }
        public string BranchName { get; set; }
        public string SectorName { get; set; }
        public string TransactionSubTypeName { get; set; }
    }
}
