using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data.Objects
{
    public class Account
    {
        #region Constructors
        public Account()
        {
            this.IdAccount = -1;
            this.Account_Name = "";
            this.Password = "";
            this.Active = false;
        }
        public Account(long id)
        {
            Load(id);
        }
        #endregion

        #region Variables
        public int IdAccount;
        public string Account_Name;
        public string Password;
        public bool Active;
        #endregion

        #region Methods  
        public void Save()
        {
            try
            {
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine(" INSERT INTO [DBO].[Account] (ID_ACCOUNT, ACCOUNT_NAME, ACCOUNT_PASSWORD, ACTIVE) ");
                sqlQuery.AppendLine($" VALUES ({GetNewId()}, '{this.Account_Name}', '{this.Password}', 1 ) ");

                Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void Load(string username, long key)
        {
            try
            {
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine(" SELECT ID_ACCOUNT, ACCOUNT_NAME, ACCOUNT_PASSWORD, ACTIVE FROM [DBO].[ACCOUNT](NOLOCK) ");
                sqlQuery.AppendLine($" WHERE  ACCOUNT_NAME = '{username}' AND ACCOUNT_PASSWORD = '{key}' ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
                if (obj == null)
                {
                    this.IdAccount = -1;
                    this.Account_Name = "";
                    this.Password = "";
                    this.Active = false;
                    return;
                }
                else
                {
                    this.IdAccount = obj.Field<int>("ID_ACCOUNT");
                    this.Account_Name = obj.Field<string>("ACCOUNT_NAME");
                    this.Password = obj.Field<string>("ACCOUNT_PASSWORD");
                    this.Active = obj.Field<bool>("ACTIVE");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void Load(long id)
        {
            try
            {
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine(" SELECT ID_ACCOUNT, ACCOUNT_NAME, ACCOUNT_PASSWORD, ACTIVE FROM [DBO].[ACCOUNT](NOLOCK) ");
                sqlQuery.AppendLine($" WHERE ID_ACCOUNT = '{id}' ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
                if (obj == null)
                {
                    this.IdAccount = -1;
                    this.Account_Name = "";
                    this.Password = "";
                    this.Active = false;
                    return;
                }
                else
                {
                    this.IdAccount = obj.Field<int>("ID_ACCOUNT");
                    this.Account_Name = obj.Field<string>("ACCOUNT_NAME");
                    this.Password = obj.Field<string>("ACCOUNT_PASSWORD");
                    this.Active = obj.Field<bool>("ACTIVE");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        private int GetNewId()
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine(" SELECT (ID_ACCOUNT+1) AS ID_ACCOUNT FROM [DBO].[ACCOUNT](NOLOCK) ");
            sqlQuery.AppendLine($" ORDER BY ID_ACCOUNT DESC ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
            int rowId = obj.Field<int>("ID_ACCOUNT");
            return rowId;
        }
        #endregion
    }
}
