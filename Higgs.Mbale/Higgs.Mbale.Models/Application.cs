﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
 public   class Application
    {
        
            public long ApplicationId { get; set; }
            public string Name { get; set; }
            public double TotalCash { get; set; }
            public System.DateTime TimeStamp { get; set; }
        
    }
}
