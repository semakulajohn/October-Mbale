using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;
namespace Higgs.Mbale.DAL.Interface
{
 public   interface IFlourTransferDataService
    {
            IEnumerable<FlourTransfer> GetAllFlourTransfers();
            FlourTransfer GetFlourTransfer(long flourTransferId);
            long SaveFlourTransfer(FlourTransferDTO flourTransfer, string userId);
            void MarkAsDeleted(long flourTransferId, string userId);
            IEnumerable<FlourTransfer> GetAllFlourTransfersForAParticularStore(long storeId);
            void SaveFlourTransferGradeSize(FlourTransferGradeSizeDTO flourTransferGradeSizeDTO);
            void PurgeFlourTransferGradeSize(long flourTransferId);
            int SaveStoreFlourTransferGradeSize(StoreGradeSizeDTO storeGradeSizeDTO, bool inOrOut);
            IEnumerable<StoreGradeSize> GetStoreFlourTransferStock(long storeId);
    }
}
