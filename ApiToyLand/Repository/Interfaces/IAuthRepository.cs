using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiToyLand.Repository.Interfaces
{
    public interface IAuthRepository
    {
        bool LogIn(string username, string password);
        int Validate();
        LibraryToyLand.Data.Objects.Account GetData();
    }
}