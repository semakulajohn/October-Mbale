﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.DTO
{
 public   class BatchOutPutDTO
    {
        public long BatchOutPutId { get; set; }
        public long BatchId { get; set; }
        public double FlourOutPut { get; set; }
        public double LossPercentage { get; set; }
        public double FlourPercentage { get; set; }
        public double BrandPercentage { get; set; }
        public double BrandOutPut { get; set; }
        public Nullable<double> Loss { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public long BranchId { get; set; }
        public long SectorId { get; set; }
    }
}
