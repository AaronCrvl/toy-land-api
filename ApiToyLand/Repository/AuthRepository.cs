using ApiToyLand.Repository.Interfaces;
using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiToyLand.Repository
{
    public class AuthRepository : IAuthRepository
    {
        #region Properties
        public int IdAccount;
        public bool Active;
        public string Username;
        private string First_Name;
        private string Last_Name;
        private string Password;
        public bool logged = false;
        #endregion

        #region Methods
        public bool LogIn(string username, string password)
        {
            var ac = new LibraryToyLand.Data.Objects.Account();
            ac.Load(username, password);

            if (ac.IdAccount > 0)
            {
                this.IdAccount = ac.IdAccount;
                this.First_Name = ac.First_Name;
                this.Last_Name = ac.Last_Name;
                this.Username = ac.Username;
                this.Password = ac.Password;
                this.Active = ac.Active;

                this.logged = true;
                return logged;
            }
            else
                return logged;
        }
        public bool LogIn(int id)
        {
            var ac = new LibraryToyLand.Data.Objects.Account();
            ac.Load(id);

            if (ac.IdAccount > 0)
            {
                this.IdAccount = ac.IdAccount;
                this.First_Name = ac.First_Name;
                this.Last_Name = ac.Last_Name;
                this.Username = ac.Username;
                this.Password = ac.Password;
                this.Active = ac.Active;

                this.logged = true;
                return logged;
            }
            else
                return logged;
        }
        public int Validate()
        {
            if (this.IdAccount < 0)
                return 404;
            if (this.IdAccount > 0 && !this.Active)
                return 401;
            if (this.IdAccount > 0 && this.Active)
                return 200;

            return 400;
        }
        public Account GetData()
        {
            if (this.logged)
            {
                var ac = new LibraryToyLand.Data.Objects.Account();
                ac.Load(this.Username, this.Password);
                return ac;
            }
            else
            {
                LogIn(this.Username, this.Password);
                var ac = new LibraryToyLand.Data.Objects.Account();
                ac.Load(this.Username, this.Password);
                return ac;
            }
        }
        #endregion        
    }
}