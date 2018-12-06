using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DAL.Interface;
using log4net;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.EF.UnitOfWork;
using Higgs.Mbale.DTO;

namespace Higgs.Mbale.DAL.Concrete
{
public    class RequistionDataService : DataServiceBase,IRequistionDataService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(RequistionDataService));

       public RequistionDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Requistion> GetAllRequistions()
        {
            return this.UnitOfWork.Get<Requistion>().AsQueryable().Where(e => e.Deleted == false); 
        }
        public IEnumerable<Requistion> GetAllRequistionsForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Requistion>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }
      public  IEnumerable<Requistion> GetAllRequistionsForAParticularStatus(long statusId)
        {
            return this.UnitOfWork.Get<Requistion>().AsQueryable().Where(r => r.Deleted == false && r.StatusId == statusId);
        }
        public Requistion GetRequistion(long requistionId)
        {
            return this.UnitOfWork.Get<Requistion>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.RequistionId == requistionId &&
                    c.Deleted == false
                );
        }

        public Requistion GetLatestCreatedRequistion()
        {
            Requistion requistion = new Requistion();
            var requistions = this.UnitOfWork.Get<Requistion>().AsQueryable().Where(e => e.Deleted == false); 
            if (requistions.Any())
            {
                requistion = requistions.AsQueryable().OrderByDescending(e => e.CreatedOn).First();
                return requistion;
            }
            else
            {
                return requistion;
            }

        }
        /// <summary>
        /// Saves a new Requistion or updates an already existing Requistion.
        /// </summary>
        /// <param name="Requistion">Requistion to be saved or updated.</param>
        /// <param name="RequistionId">RequistionId of the Requistion creating or updating</param>
        /// <returns>RequistionId</returns>
        public long SaveRequistion(RequistionDTO requistionDTO, string userId)
        {
            long requistionId = 0;
            
            if (requistionDTO.RequistionId == 0)
            {
           
                var requistion = new Requistion()
                {
       
                    RequistionId = requistionDTO.RequistionId,
                    StatusId = requistionDTO.StatusId,
                    BranchId = requistionDTO.BranchId,
                    Response = requistionDTO.Response,
                    Amount = requistionDTO.Amount,
                    Description = requistionDTO.Description,
                    Approved = requistionDTO.Approved,
                    Rejected = requistionDTO.Rejected,
                    ApprovedById = requistionDTO.ApprovedById,
                    AmountInWords = requistionDTO.AmountInWords,
                    RequistionNumber = requistionDTO.RequistionNumber,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<Requistion>().AddNew(requistion);
                this.UnitOfWork.SaveChanges();
                requistionId = requistion.RequistionId;
                return requistionId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Requistion>().AsQueryable()
                    .FirstOrDefault(e => e.RequistionId == requistionDTO.RequistionId);
                if (result != null)
                {
                    result.RequistionId = requistionDTO.RequistionId;
                    result.StatusId = requistionDTO.StatusId;
                    result.ApprovedById = requistionDTO.ApprovedById;
                    result.Amount = requistionDTO.Amount;
                    result.UpdatedBy = userId;
                    result.Response = requistionDTO.Response;
                    result.Approved = requistionDTO.Approved;
                    result.Rejected = requistionDTO.Rejected;
                    result.BranchId = requistionDTO.BranchId;
                    result.RequistionNumber = requistionDTO.RequistionNumber;
                    result.AmountInWords = requistionDTO.AmountInWords;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = requistionDTO.Deleted;
                    result.DeletedBy = requistionDTO.DeletedBy;
                    result.DeletedOn = requistionDTO.DeletedOn;

                    this.UnitOfWork.Get<Requistion>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return requistionDTO.RequistionId;
            }           
        }

        public void MarkAsDeleted(long requistionId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
                dbContext.Mark_Requistion_AsDeleted(requistionId, userId);
            }


        }

   public void  UpdateRequistionWithCompletedStatus(long requistionId, long statusId, string userId)
   {
       using (var dbContext = new MbaleEntities())
       {
           dbContext.UpdateRequistionWithCompletedStatus(requistionId, statusId, userId);
       }
   }

    }
}
