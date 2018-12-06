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
 public   class DocumentService : IDocumentService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(DocumentService));
        private IDocumentDataService _dataService;
        private IUserService _userService;
        

        public DocumentService(IDocumentDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        
        public Document GetDocument(long documentId)
        {
            var result = this._dataService.GetDocument(documentId);
            return MapEFToModel(result);
        }

        public Document GetDocumentForAParticularItem(long itemId)
        {
            var result = this._dataService.GetDocumentForAParticularItem(itemId);
            return MapEFToModel(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Document> GetAllDocuments()
        {
            var results = this._dataService.GetAllDocuments();
            return MapEFToModel(results);
        }

        public IEnumerable<Document> GetAllDocumentsForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllDocumentsForAParticularBranch(branchId);
            return MapEFToModel(results);
        }

        public IEnumerable<Document> GetAllDocumentsForAParticularCategory(long documentCategoryId)
        {
            var results = this._dataService.GetAllDocumentsForAParticularCategory(documentCategoryId);
            return MapEFToModel(results);
        } 

     private long GetDocumentNumber(long documentCategoryId)
     {
         long documentNumber = 0;
         var latestDocument = _dataService.GetLatestCreatedDocumentForAParticularCategory(documentCategoryId);
         if(latestDocument !=  null){
            documentNumber = latestDocument.DocumentNumber + 1;
         }
         else
         {
             documentNumber = documentNumber + 1;
             
         }
        return documentNumber;
     }
       
        public long SaveDocument(Document document, string userId)
        {
            long documentNumber = 0;
            if (document.DocumentId == 0)
            {
                documentNumber = GetDocumentNumber(document.DocumentCategoryId);       

            }
            else
            {
                documentNumber = document.DocumentNumber;
            }
           
                 var documentDTO = new DTO.DocumentDTO()
                {
                    DocumentId = document.DocumentId,
                   
                    UserId = document.UserId,
                    DocumentCategoryId = document.DocumentCategoryId,
                    Amount = document.Amount,
                    BranchId = document.BranchId,
                    ItemId = document.ItemId,
                    Description = document.Description,
                    Quantity = document.Quantity,
                    DocumentNumber = documentNumber,
                    AmountInWords = document.AmountInWords,
                    CreatedOn = document.CreatedOn,
                    TimeStamp = document.TimeStamp,
                    CreatedBy = document.CreatedBy,
                    
                };    

           var documentId = this._dataService.SaveDocument(documentDTO, userId);

           return documentId;
                      
        }

        
        public void MarkAsDeleted(long documentId, string userId)
        {
            _dataService.MarkAsDeleted(documentId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Document> MapEFToModel(IEnumerable<EF.Models.Document> data)
        {
            var list = new List<Document>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }


        public Document MapEFToModel(EF.Models.Document data)
        {

            var userName = string.Empty;
            if (data != null)
            {
                var user = _userService.GetAspNetUser(data.UserId);
                if (user != null)
                {
                    userName = user.FirstName + ' ' + user.LastName;
                }
                var document = new Document()
                   {
                       DocumentId = data.DocumentId,
                       UserId = data.UserId,
                       DocumentCategoryId = data.DocumentCategoryId,
                       DocumentCategoryName = data.DocumentCategory != null ? data.DocumentCategory.Name : "",
                       Amount = data.Amount,
                       ItemId = data.ItemId,
                       BranchId = data.BranchId,
                       BranchName = data.Branch != null ? data.Branch.Name : "",
                       Description = data.Description,
                       Quantity = Convert.ToDouble(data.Quantity),
                       UserName = userName,
                       DocumentNumber = data.DocumentNumber,
                       AmountInWords = data.AmountInWords,
                       CreatedOn = data.CreatedOn,
                       TimeStamp = data.TimeStamp,
                       CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                       UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),

                   };

                return document;
            }
            else
            {
                return null;
            }

        }


       #endregion
    }
}
