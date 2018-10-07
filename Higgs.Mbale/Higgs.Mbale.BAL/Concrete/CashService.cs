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

namespace Higgs.Mbale.BAL.Concrete
{
 public   class CashService : ICashService
    {
          ILog logger = log4net.LogManager.GetLogger(typeof(CashService));
        private ICashDataService _dataService;
        private IUserService _userService;
        private ITransactionSubTypeService _transactionSubTypeService;
        private IDocumentService _documentService;
        private ITransactionDataService _transactionDataService;
       


        public CashService(ICashDataService dataService, IUserService userService,IDocumentService documentService,
            ITransactionSubTypeService transactionSubTypeService,
            ITransactionDataService transactionDataService)
            
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionSubTypeService = transactionSubTypeService;
             this._transactionDataService = transactionDataService;
             this._documentService = documentService;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CashId"></param>
        /// <returns></returns>
        public Cash GetCash(long cashId)
        {
            var result = this._dataService.GetCash(cashId);
            return MapEFToModel(result);
        }

    
        public IEnumerable<Cash> GetAllCashForAParticularBranch(long branchId)
        {
           
                var results = this._dataService.GetAllCashForAParticularBranch(branchId);
                return MapEFToModel(results);
            
            
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cash> GetAllCash()
        {
            var results = this._dataService.GetAllCash();
            return MapEFToModel(results);
        }

        private double GetBalanceForLastCash(long branchId)
        {
            double balance = 0;
           
                var result = this._dataService.GetLatestCashForAParticularBranch(branchId);
                if (result.BranchId > 0)
                {
                    balance = result.Balance;
                }
               
                return balance;
            
           
        }

       
        public long SaveCash(Cash cash, string userId)
        {
            long cashId = 0;
            double startAmount =0;
            double OldBalance = 0;
            double NewBalance = 0;
            
               
               OldBalance = GetBalanceForLastCash(cash.BranchId);
               startAmount = OldBalance;
           

                if (cash.Action == "-")
                {
                    NewBalance = OldBalance - cash.Amount;
                }
                else
                {
                    NewBalance = OldBalance + cash.Amount;
                }

                var cashDTO = new DTO.CashDTO()
                {
                   
                    Amount = cash.Amount,
                    StartAmount = startAmount,
                    Balance = NewBalance,
                    Notes = cash.Notes,
                    CashId = cash.CashId,
                    Action = cash.Action,
                    BranchId = cash.BranchId,
                    TransactionSubTypeId = cash.TransactionSubTypeId,
                    SectorId = cash.SectorId,
                    Deleted = cash.Deleted,
                    CreatedBy = cash.CreatedBy,
                    CreatedOn = cash.CreatedOn

                };

                 cashId = this._dataService.SaveCash(cashDTO, userId);

                 SaveApplicationCash(cash.Amount, cash.Action);

               //var document =new  Document()
               // {
                   
               //     Name = document.Name,
               //     UserId = document.UserId,
               //     DocumentCategoryId = document.DocumentCategoryId,
               //     Amount = document.Amount,
               //     BranchId = document.BranchId,
               //     Description = document.Description,
               //     Quantity = document.Quantity,
               //     DocumentNumber = documentNumber,
                    
               //};
               //var documentId = _documentService.SaveDocument(document, userId);

                 long transactionTypeId = 0;
                 List<string> transactionSubTypeNames = new List<string>();
                 var transactionSubType = _transactionSubTypeService.GetTransactionSubType(cash.TransactionSubTypeId);

                 if (transactionSubType != null)
                 {
                     transactionTypeId = transactionSubType.TransactionTypeId;
                   
                     var transaction = new TransactionDTO()
                     {
                         BranchId = Convert.ToInt64(cash.BranchId),
                         SectorId = cash.SectorId,
                         Amount = cash.Amount,
                         TransactionSubTypeId = cash.TransactionSubTypeId,
                         TransactionTypeId = transactionTypeId,
                         CreatedOn = DateTime.Now,
                         TimeStamp = DateTime.Now,
                         CreatedBy = userId,
                         Deleted = false,

                     };
                     var transactionId = _transactionDataService.SaveTransaction(transaction, userId);
                 }
            return cashId;
                 }

        public void SaveApplicationCash(double amount, string action)
        {
            var application = _dataService.GetApplicationDetails();
            double startAmount = 0;
            double OldBalance = 0;
            double newBalance = 0;

            if (application != null)
            {
                OldBalance = application.TotalCash;
                startAmount = OldBalance;

                if (action == "-")
                {
                    newBalance = OldBalance - amount;
                }
                else
                {
                    newBalance = OldBalance + amount;
                }

                var applicatinDTO = new DTO.ApplicationDTO()
                {
                    ApplicationId = application.ApplicationId,
                    TotalCash = newBalance,
                    Name = application.Name,
                    TimeStamp = application.TimeStamp,

                };
                this._dataService.UpdateApplicationCash(applicatinDTO);
            }
           



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cashId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long cashId, string userId)
        {
            _dataService.MarkAsDeleted(cashId, userId);
        }

      
        #region Mapping Methods

        public IEnumerable<Cash> MapEFToModel(IEnumerable<EF.Models.Cash> data)
        {
            var list = new List<Cash>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }


        public Cash MapEFToModel(EF.Models.Cash data)
        {
            var accountName = string.Empty;
            
         

            var cash = new Cash()
            {
               
                Action = data.Action,
                StartAmount= data.StartAmount,
                Balance = data.Balance,
                Amount = data.Amount,
                Notes = data.Notes,
                CashId = data.CashId,
                BranchId = data.BranchId,
                BranchName = data.Branch != null?data.Branch.Name:"",
                SectorId = data.SectorId,
                SectorName  = data.Sector != null?data.Sector.Name:"",
                TransactionSubTypeId = data.TransactionSubTypeId,
                TransactionSubTypeName = data.TransactionSubType !=null?data.TransactionSubType.Name:"",
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
               
                            

            };
            return cash;
        }

     
       #endregion

       
    }
}
