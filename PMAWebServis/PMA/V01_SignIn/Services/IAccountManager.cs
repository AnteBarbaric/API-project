using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Models;
using Models.Accounts;

namespace Services
{
    public interface IAccountManager
    {
        OperationResult<AccountListModel> GetAll();
        OperationResult<AccountModel> Get(int userLoginId);
        OperationResult<ResponseModel> Create(AccountModel model);
        OperationResult<ResponseModel> Update(int userLoginId, AccountModel model);
        OperationResult<ResponseModel> Change(int userLoginId, AccountPatchModel model);
        OperationResult<ResponseModel> Delete(int userLoginId);
    }
}
