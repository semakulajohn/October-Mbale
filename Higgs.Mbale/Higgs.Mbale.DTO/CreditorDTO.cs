﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
   public class CreditorDTO
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
    }
}
