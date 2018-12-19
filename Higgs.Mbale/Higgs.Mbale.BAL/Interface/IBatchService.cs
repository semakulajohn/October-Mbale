using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
  public  interface IBatchService
    {
        IEnumerable<Batch> GetAllBatches();
        Batch GetBatch(long batchId);
        long SaveBatch(Batch batch, string userId);
        void MarkAsDeleted(long batchId, string userId);
        IEnumerable<Batch> GetAllBatchesForAParticularBranch(long branchId);
        IEnumerable<Batch> MapEFToModel(IEnumerable<EF.Models.Batch> data);
        IEnumerable<LabourCost> GenerateLabourCosts(long batchId,string userId);
        IEnumerable<Batch> GetBatchesForAParticularBranchToTransfer(long branchId,long productId);
        void UpdateBatchBrandBalance(long batchId, double quantity, string userId);
        IEnumerable<Batch> GetAllBatchesForBrandDelivery(long branchId);
        void UpdateBatchGradeSizes(List<BatchGradeSize> batchGradeSizeList);
       
    }
}
