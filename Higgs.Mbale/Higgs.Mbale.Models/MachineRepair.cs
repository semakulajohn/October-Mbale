﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
    public class MachineRepair
    {
        public long MachineRepairId { get; set; }
        public string NameOfRepair { get; set; }
        public double Amount { get; set; }
        public System.DateTime DateRepaired { get; set; }
        public long TransactionSubTypeId { get; set; }
        public long SectorId { get; set; }
        public long BatchId { get; set; }
        public string Description { get; set; }
        public long BranchId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public string TransactionSubTypeName { get; set; }
        public string BranchName { get; set; }
        public string SectorName { get; set; }
        public string BatchNumber { get; set; }
    }
}
