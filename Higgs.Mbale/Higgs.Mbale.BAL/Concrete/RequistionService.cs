using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;
using log4net;
using System.Web;
using System.Configuration;
using System.IO;

namespace Higgs.Mbale.BAL.Concrete
{
 public   class RequistionService : IRequistionService
    {
     private long requistionStatusIdComplete = Convert.ToInt64(ConfigurationManager.AppSettings["StatusIdComplete"]);
     private long paymentVoucherId = Convert.ToInt64(ConfigurationManager.AppSettings["PaymentVoucher"]);
     private long debitId = Convert.ToInt64(ConfigurationManager.AppSettings["DebitId"]);
     private long sectorId = Convert.ToInt64(ConfigurationManager.AppSettings["SectorId"]);
       ILog logger = log4net.LogManager.GetLogger(typeof(RequistionService));
        private IRequistionDataService _dataService;
        private IUserService _userService;
        private IDocumentService _documentService;
        private ICashService _cashService;
        

        public RequistionService(IRequistionDataService dataService,IUserService userService,IDocumentService documentService,
            ICashService cashService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._documentService = documentService;
            this._cashService = cashService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequistionId"></param>
        /// <returns></returns>
        public Requistion GetRequistion(long requistionId)
        {
            var result = this._dataService.GetRequistion(requistionId);
            return MapEFToModel(result);
        }

        public IEnumerable<Requistion> GetAllRequistionsForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllRequistionsForAParticularBranch(branchId);
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Requistion> GetAllRequistions()
        {
            var results = this._dataService.GetAllRequistions();
            return MapEFToModel(results);
        }

        public IEnumerable<Requistion> GetAllRequistionsForAParticularStatus(long statusId)
        {
            var results = this._dataService.GetAllRequistionsForAParticularStatus(statusId);
            return MapEFToModel(results);
        }




        private long GetRequistionNumber()
        {
            long requistionNumber = 0;
            var latestRequistion = _dataService.GetLatestCreatedRequistion();
            if (latestRequistion != null)
            {
                requistionNumber = Convert.ToInt64(latestRequistion.RequistionNumber) + 1;
            }
            else
            {
                requistionNumber = requistionNumber + 1;

            }
            return requistionNumber;
        }
        public long SaveRequistion(Requistion requistion, string userId)
        {
            long requistionId = 0;
            //long requistionNumber = 0;
            //if (requistion.RequistionId == 0)
            //{
            //    requistionNumber = GetRequistionNumber();

            //}
            //else
            //{
            //    requistionNumber = Convert.ToInt64(requistion.RequistionNumber);
            //}
            var requistionDTO = new DTO.RequistionDTO()
            {
                RequistionId = requistion.RequistionId,
                Response = requistion.Response,
                StatusId = requistion.StatusId,
                RequistionNumber = requistion.RequistionNumber,
                Amount = requistion.Amount,
                Approved = requistion.Approved,
                AmountInWords = requistion.AmountInWords,
                Rejected = requistion.Rejected,
                ApprovedById = requistion.ApprovedById,
                BranchId = requistion.BranchId,
                Description = requistion.Description,
                Deleted = requistion.Deleted,
                CreatedBy = requistion.CreatedBy,
                CreatedOn = requistion.CreatedOn,

            };

          
           if (requistion.Approved && requistion.ApprovedById != null)
           {
               var cash = new Cash()
               {

                   Amount = requistion.Amount,
                   Notes = requistion.Description,
                   Action = "-",
                   BranchId = requistion.BranchId,
                   TransactionSubTypeId = debitId,
                   SectorId = sectorId,

                   CreatedBy = requistion.ApprovedById,

               };
              var  cashId = _cashService.SaveCash(cash, userId);
              if (cashId == -1)
              {
                  requistionId = cashId;
                  return requistionId;
              }
              else
              {
                   requistionId = this._dataService.SaveRequistion(requistionDTO, userId);
                  UpdateRequistion(requistion.RequistionId, requistionStatusIdComplete, requistion.ApprovedById);

                  var document = new Document()
                  {
                      DocumentId = 0,

                      UserId = requistion.CreatedBy,
                      DocumentCategoryId = paymentVoucherId,
                      Amount = requistion.Amount,
                      BranchId = requistion.BranchId,
                      ItemId = requistionId,
                      Description = requistion.Description,
                      AmountInWords = requistion.AmountInWords,

                  };

                  var documentId = _documentService.SaveDocument(document, userId);
              }
             
             
           }
           else
           {
           //    SendEmail(requistionDTO, userId);
                requistionId = this._dataService.SaveRequistion(requistionDTO, userId);
           }
          
           return requistionId;
                      
        }

        public void SendEmail(RequistionDTO requistion,string userId)
        {
            DateTime createdOn = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            string strNewPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RequistionEmail"]);
            using (StreamReader sr = new StreamReader(strNewPath))
            {
                while (!sr.EndOfStream)
                {
                    sb.Append(sr.ReadLine());
                }
            }



            string body = sb.ToString().Replace("#REQUISTIONNUMBER#", requistion.RequistionNumber);
            body = body.Replace("#DESCRIPTION#", requistion.Description);
            body = body.Replace("#CREATEDBY#", userId);
            body = body.Replace("#CREATEDON#", Convert.ToString(createdOn));

            Helpers.Email email = new Helpers.Email();
            email.MailBodyHtml = body;
            email.MailToAddress = ConfigurationManager.AppSettings["administrator-email"];
            email.MailFromAddress = ConfigurationManager.AppSettings["EmailAddressFrom"];
            email.Subject = ConfigurationManager.AppSettings["requistion_email_subject"];
            email.SendMail();
            logger.Debug("Email sent");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequistionId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long requistionId, string userId)
        {
            _dataService.MarkAsDeleted(requistionId, userId);
        }

        private void UpdateRequistion(long requistionId, long statusId, string userId)
        {
            var requistion = _dataService.GetRequistion(requistionId);
            if (requistion !=null)
            {
                    _dataService.UpdateRequistionWithCompletedStatus(requistionId, statusId, userId);
                
            }

        }
      
        #region Mapping Methods

        private IEnumerable<Requistion> MapEFToModel(IEnumerable<EF.Models.Requistion> data)
        {
            var list = new List<Requistion>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Requistion EF object to Requistion Model Object and
        /// returns the Requistion model object.
        /// </summary>
        /// <param name="result">EF Requistion object to be mapped.</param>
        /// <returns>Requistion Model Object.</returns>
        public Requistion MapEFToModel(EF.Models.Requistion data)
        {
            string statusName = string.Empty;
            long documentId = 0;
            if (data != null)
            {
                var document = _documentService.GetDocumentForAParticularItem(data.RequistionId);
                if (document != null)
                {
                    documentId = document.DocumentId;
                }
                if (data.Status != null)
                {
                    if (data.Status.StatusId == 2)
                    {
                        statusName = "Approved";
                    }
                    else
                    {
                        statusName = data.Status.Name;
                    }

                }
                var requistion = new Requistion()
                {
                    RequistionId = data.RequistionId,
                    ApprovedById = data.ApprovedById,
                    StatusId = data.StatusId,
                    Amount = data.Amount,
                    Response = data.Response,
                    BranchId = data.BranchId,
                    BranchName = data.Branch != null ? data.Branch.Name : "",
                    StatusName = statusName,
                    ApprovedByName = _userService.GetUserFullName(data.AspNetUser),
                    RequistionNumber = data.RequistionNumber,
                    Description = data.Description,
                    CreatedOn = data.CreatedOn,
                    TimeStamp = data.TimeStamp,
                    Approved = data.Approved,
                    Rejected = data.Rejected,
                    AmountInWords = data.AmountInWords,
                    Deleted = data.Deleted,
                    CreatedBy = _userService.GetUserFullName(data.AspNetUser1),
                    UpdatedBy = _userService.GetUserFullName(data.AspNetUser2),
                    DocumentId = documentId,


                };
                return requistion;
            }
            return null;
        }



       #endregion
    }
}
