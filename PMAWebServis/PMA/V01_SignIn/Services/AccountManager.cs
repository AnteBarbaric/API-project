using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Models;
using Models.Accounts;
using V01_SignIn.Data;
using V01_SignIn.Data.Models;

namespace Services
{
    public class AccountManager : IAccountManager
    {
        protected TESTContext db;
        protected Repository<UserLogin> userLoginRepo;
        public AccountManager()
        {
            this.db = new TESTContext();
            this.userLoginRepo = new Repository<UserLogin>(this.db);
        }
        public OperationResult<AccountListModel> GetAll()
        {
            OperationResult<AccountListModel> result = new OperationResult<AccountListModel>(new AccountListModel());

            IEnumerable<UserLogin> list = this.userLoginRepo.GetAll(result);

            foreach(var item in list)
            {
                AccountModel model = new AccountModel()
                {
                    UserLoginId = item.UserLoginId,
                    Username = item.Username,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Phone = item.Phone,
                    LanguageId = item.LanguageId,
                    UserRoleId = item.UserRoleId,
                    StateId = item.StateId
                };
                result.Result.Items.Add(model);
            }
            return result;
        }
        public OperationResult<AccountModel> Get(int userLoginId)
        {
            OperationResult<AccountModel> result = new OperationResult<AccountModel>(new AccountModel());

            UserLogin entity = this.userLoginRepo.GetAll(result)
                .Where(x => x.UserLoginId == userLoginId)
                .SingleOrDefault();
            if(entity != null)
            {
                result.Result.UserLoginId = entity.UserLoginId;
                result.Result.FirstName = entity.FirstName;
                result.Result.LastName = entity.LastName;
                result.Result.Email = entity.Email;
                result.Result.Phone = entity.Phone;
                result.Result.LanguageId = entity.LanguageId;
                result.Result.UserRoleId = entity.UserRoleId;
                result.Result.StateId = entity.StateId;
            }
            return result;
        }
        public OperationResult<ResponseModel> Create(AccountModel model)
        {
            OperationResult<ResponseModel> result = new OperationResult<ResponseModel>(new ResponseModel());

            UserLogin entity = new UserLogin();
            entity.Username = model.Username;
            entity.Password = model.Password;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.Phone = model.Phone;
            entity.LanguageId = model.LanguageId != "0" ? model.LanguageId : null;
            entity.UserRoleId = model.UserRoleId;
            entity.StateId = model.StateId;
            entity.Created = DateTime.Now;

            this.userLoginRepo.Insert(entity, result);
            if(result.Succeeded)
            {
                model.UserLoginId = entity.UserLoginId;
            }

            return result;
        }
        public OperationResult<ResponseModel> Update(int userLoginId, AccountModel model)
        {
            OperationResult<ResponseModel> result = new OperationResult<ResponseModel>(new ResponseModel());

            UserLogin entity = this.userLoginRepo.GetAll(result)
               .Where(x => x.UserLoginId == userLoginId)
               .SingleOrDefault();
            if (entity != null)
            {
                entity.Username = model.Username;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Email = model.Email;
                entity.Phone = model.Phone;
                entity.LanguageId = model.LanguageId != "0" ? model.LanguageId : null;
                entity.UserRoleId = model.UserRoleId;
                entity.StateId = model.StateId;
                entity.Changed = DateTime.Now;

                this.userLoginRepo.Update(entity, result);
            }
            else
            {
                result.Errors.Add(string.Format("Korisnik id={0} nije pronađen!", userLoginId));
            }
            return result;
        }

        public OperationResult<ResponseModel> Change(int userLoginId, AccountPatchModel model)
        {
            OperationResult<ResponseModel> result = new OperationResult<ResponseModel>(new ResponseModel());

            UserLogin entity = this.userLoginRepo.SearchFor(x => x.UserLoginId == userLoginId).FirstOrDefault();

            if(!result.Succeeded)
            {
                return (result);
            }
            if(entity == null)
            {
                result.Errors.Add("Korisnik nije pronađen");
                return result;
            }
            switch(model.Target)
            {
                case "language":
                    entity.LanguageId = !string.IsNullOrEmpty(model.Val) ? model.Val : null;
                    break;
                case "role":
                    int? r = V01_SignIn.Util.ToInt32(model.Val, null);
                    if( r == null || r == 0)
                    {
                        r = 2;
                    }
                    entity.UserRoleId = (int)r;
                    break;
                case "state":
                    entity.StateId = model.Val;
                    break;
            }
            this.userLoginRepo.Update(entity, result);
            return result;
        }

        public OperationResult<ResponseModel> Delete(int userLoginId)
        {
            OperationResult<ResponseModel> result = new OperationResult<ResponseModel>(new ResponseModel());

            UserLogin entity = this.userLoginRepo.GetAll(result)
               .Where(x => x.UserLoginId == userLoginId)
               .SingleOrDefault();

            if (entity != null)
            {
                this.userLoginRepo.Delete(entity, result);
            }
            else
            {
                result.Errors.Add(string.Format("Korisnik id={0} nije pronađen!", userLoginId));
            }
            return result;
        }
    }
}
