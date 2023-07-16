using ApiToyLand.Models;
using ApiToyLand.Repository.Interfaces;
using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiToyLand.Repository
{
    public class AccountRepository : IAccountRepository
    {
        #region Constructor
        public AccountRepository()
        {
            Auth = new AuthRepository();
        }
        #endregion

        #region Properties
        public AuthRepository Auth
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        #endregion

        #region Methods
        public Account GetData()
        {
            if (Auth.logged)
            {
                var ac = new LibraryToyLand.Data.Objects.Account();
                ac.Load(Auth.IdAccount);
                return ac;
            }
            else
            {
                Auth.LogIn(Auth.IdAccount);
                var ac = new LibraryToyLand.Data.Objects.Account();
                ac.Load(Auth.IdAccount);
                return ac;
            }
        }
        public bool LogIn(int id)
        {
            Auth.LogIn(id);
            if (Auth.Active)
                return true;
            else
                return false;
        }
        public void CreateAccount(newAccountModel model)
        {
            Account ac = new Account();
            ac.Username = model.Username;
            ac.First_Name = model.FirstName;
            ac.Last_Name = model.LastName;
            ac.Password = model.Password;
            ac.Active = true;
            ac.SaveNew();
        }
        public void AlterAccount(AccountModel model)
        {
            Account ac = new Account();
            ac.Load(model.IdAccount);
            
            ac.Username = model.UserName;
            ac.First_Name = model.FirstName;
            ac.Last_Name = model.LastName;
            ac.Password = model.Password;
            ac.Active = true;
            ac.Save();
        }
        public bool testUsername(string username)
        {
            var testAcct = new Account();
            testAcct.LoadByUsername(username);

            if (testAcct.IdAccount < 0)
                return true;
            else
                return false;
        }
        public bool Validate(newAccountModel model)
        {
            if (string.IsNullOrEmpty(model.FirstName)
                    || string.IsNullOrEmpty(model.LastName)
                    || string.IsNullOrEmpty(model.Username))
            {
                return true;
            }
            else
                return false;
        }
        public bool Validate(AccountModel model)
        {
            if (string.IsNullOrEmpty(model.FirstName)
                    || string.IsNullOrEmpty(model.LastName)
                    || string.IsNullOrEmpty(model.UserName))
            {
                return true;
            }
            else
                return false;
        }

        public string GetAccountName(int id)
        {
            var account = new Account(id);
            if (id > 0)
                return account.First_Name + " " + account.Last_Name;
            else
                return "Account Not Found";
        }
        #endregion
    }
}