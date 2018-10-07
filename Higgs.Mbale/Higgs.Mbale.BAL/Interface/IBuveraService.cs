using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
  public  interface IBuveraService
    {
        IEnumerable<Buvera> GetAllBuveras();
        Buvera GetBuvera(long buveraId);
        long SaveBuvera(Buvera buvera, string userId);
        void MarkAsDeleted(long buveraId, string userId);
        IEnumerable<Buvera> GetAllBuverasForAParticularStore(long storeId);
        StoreGrade GetStoreBuveraStock(long storeId);
               
    }
}
