using ApiToyLand.Models;
using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiToyLand.Repository.Interfaces
{
    public interface IAccountRepository 
    {        
        AuthRepository Auth { get; set; }
        Account GetData();
        bool LogIn(int id);
        void CreateAccount(newAccountModel model);
        void AlterAccount(AccountModel model);
        bool testUsername(string username);
        bool Validate(newAccountModel model);
        bool Validate(AccountModel model);
    }
}
