﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Higgs.Mbale.BAL.Interface;
using log4net;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.Web.Controllers
{
    public class CashApiController : ApiController
    {
          private ICashService _cashService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(AccountTransactionActivityApiController));
            private string userId = string.Empty;

            public CashApiController()
            {
            }

            public CashApiController(ICashService cashService, IUserService userService)
            {
                this._cashService = cashService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }


            
            [HttpGet]
            [ActionName("GetCash")]
            public Cash GetCash(long cashId)
            {
                return _cashService.GetCash(cashId);
            }

            [HttpGet]
            [ActionName("GetAllCash")]
            public IEnumerable<Cash> GetAllCash()
            {
                return _cashService.GetAllCash();
            }

            [HttpGet]
            [ActionName("GetAllCashForAParticularBranch")]
            public IEnumerable<Cash> GetAllCashForAParticularBranch(long branchId)
            {
                return _cashService.GetAllCashForAParticularBranch(branchId);
            }
         
          

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteCash(long cashId)
            {
                _cashService.MarkAsDeleted(cashId, userId);
            }

         

            [HttpPost]
            [ActionName("Save")]
            public long Save(Cash model)
            {

                var cashId = _cashService.SaveCash(model, userId);
                return cashId;
            }

          

    }
}
