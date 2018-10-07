﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
 public   interface IDocumentDataService
    {
        IEnumerable<Document> GetAllDocuments();
        Document GetDocument(long documentId);
        long SaveDocument(DocumentDTO documentDTO, string userId);
        void MarkAsDeleted(long documentId, string userId);
        IEnumerable<Document> GetAllDocumentsForAParticularCategory(long documentCategoryId);
        IEnumerable<Document> GetAllDocumentsForAParticularBranch(long branchId);
      Document GetLatestCreatedDocumentForAParticularCategory(long documentCategoryId);
    }
}
