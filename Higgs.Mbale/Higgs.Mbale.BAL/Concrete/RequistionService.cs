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
       ILog logger = log4net.LogManager.GetLogger(typeof(RequistionService));
        private IRequistionDataService _dataService;
        private IUserService _userService;
        

        public RequistionService(IRequistionDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
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
        public long SaveRequistion(Requistion requistion, string userId)
        {
            var requistionDTO = new DTO.RequistionDTO()
            {
                RequistionId = requistion.RequistionId,
                Response = requistion.Response,
                StatusId = requistion.StatusId,
                RequistionNumber = requistion.RequistionNumber,
                Amount = requistion.Amount,
                ApprovedById = requistion.ApprovedById,
                BranchId = requistion.BranchId,
                Description = requistion.Description,
                Deleted = requistion.Deleted,
                CreatedBy = requistion.CreatedBy,
                CreatedOn = requistion.CreatedOn,

            };

           var requistionId = this._dataService.SaveRequistion(requistionDTO, userId);
           if (requistion.ApprovedById != null)
           {
               UpdateRequistion(requistion.RequistionId, requistionStatusIdComplete, requistion.ApprovedById);
           }
           else
           {
               SendEmail(requistionDTO, userId);
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
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser1),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser2),
               

            };
            return requistion;
        }



       #endregion
    }
}
