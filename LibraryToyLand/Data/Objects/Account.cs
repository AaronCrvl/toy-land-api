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
            this.First_Name = "";
            this.Last_Name = "";
            this.USERNAME = "";
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
        public string First_Name;
        public string Last_Name;
        public string USERNAME;
        public string Password;
        public bool Active;
        #endregion

        #region Methods  
        public void SaveNew()
        {
            try
            {
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine(" INSERT INTO [DBO].[Account] (ID_ACCOUNT, USERNAME, ACCOUNT_PASSWORD, ACTIVE) ");
                sqlQuery.AppendLine($" VALUES ({GetNewId()}, '{this.USERNAME}', '{this.Password}', 1 ) ");

                Framework.Database.Transaction.ExecuteCreateObjectCommand(sqlQuery.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void Save()
        {
            try
            {
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine($" UPDATE [DBO].[Account] SET FIRST_NAME = '{this.First_Name}', LAST_NAME = '{this.Last_Name}', USERNAME = '{this.USERNAME}', ACCOUNT_PASSWORD = '{this.Password}' ");                
                Framework.Database.Transaction.ExecuteUpdateObjectCommand(sqlQuery.ToString());
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
                sqlQuery.AppendLine(" SELECT ID_ACCOUNT, FIRST_NAME, LAST_NAME, USERNAME, ACCOUNT_PASSWORD, ACTIVE FROM [DBO].[ACCOUNT](NOLOCK) ");
                sqlQuery.AppendLine($" WHERE  USERNAME = '{username}' AND ACCOUNT_PASSWORD = '{key}' ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
                if (obj == null)
                {
                    this.IdAccount = -1;
                    this.USERNAME = "";
                    this.First_Name = "";
                    this.Last_Name = "";
                    this.Password = "";
                    this.Active = false;
                    return;
                }
                else
                {
                    this.IdAccount = obj.Field<int>("ID_ACCOUNT");
                    this.USERNAME = obj.Field<string>("USERNAME");
                    this.First_Name = obj.Field<string>("FIRST_NAME");
                    this.Last_Name = obj.Field<string>("LAST_NAME");
                    this.Password = obj.Field<string>("ACCOUNT_PASSWORD");
                    this.Active = obj.Field<bool>("ACTIVE");
                    return;
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
                sqlQuery.AppendLine(" SELECT ID_ACCOUNT, FIRST_NAME, LAST_NAME, USERNAME, ACCOUNT_PASSWORD, ACTIVE FROM [DBO].[ACCOUNT](NOLOCK) ");
                sqlQuery.AppendLine($" WHERE ID_ACCOUNT = '{id}' ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
                if (obj == null)
                {
                    this.IdAccount = -1;
                    this.USERNAME = "";
                    this.First_Name = "";
                    this.Last_Name = "";
                    this.Password = "";
                    this.Active = false;
                    return;
                }
                else
                {
                    this.IdAccount = obj.Field<int>("ID_ACCOUNT");
                    this.USERNAME = obj.Field<string>("USERNAME");
                    this.First_Name = obj.Field<string>("FIRST_NAME");
                    this.Last_Name = obj.Field<string>("LAST_NAME");
                    this.Password = obj.Field<string>("ACCOUNT_PASSWORD");
                    this.Active = obj.Field<bool>("ACTIVE");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void LoadByUsername(string userN)
        {
            try
            {
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine(" SELECT ID_ACCOUNT, FIRST_NAME, LAST_NAME, USERNAME, ACCOUNT_PASSWORD, ACTIVE FROM [DBO].[ACCOUNT](NOLOCK) ");
                sqlQuery.AppendLine($" WHERE USERNAME = '{userN}'");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
                if (obj == null)
                {
                    this.IdAccount = -1;
                    this.USERNAME = "";
                    this.First_Name = "";
                    this.Last_Name = "";
                    this.Password = "";
                    this.Active = false;
                    return;
                }
                else
                {
                    this.IdAccount = obj.Field<int>("ID_ACCOUNT");
                    this.USERNAME = obj.Field<string>("USERNAME");
                    this.First_Name = obj.Field<string>("FIRST_NAME");
                    this.Last_Name = obj.Field<string>("LAST_NAME");
                    this.Password = obj.Field<string>("ACCOUNT_PASSWORD");
                    this.Active = obj.Field<bool>("ACTIVE");
                    return;
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
