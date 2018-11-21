using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
 public   class ReportSearch
    {
       
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string SupplierId { get; set; }
            public long BranchId { get; set; }
            public string CustomerId { get; set; }
                

        }
    
}
