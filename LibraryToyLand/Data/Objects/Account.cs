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
            this.Username = "";
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
        public string Username;
        public string Password;
        public bool Active;
        #endregion

        #region Methods  
        public void SaveNew()
        {
            try
            {                
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine(" INSERT INTO [DBO].[Account] ");
                sqlQuery.AppendLine(" ( ID_ACCOUNT, ");
                sqlQuery.AppendLine(" USERNAME, ");
                sqlQuery.AppendLine(" ACCOUNT_PASSWORD, ");
                sqlQuery.AppendLine(" ACTIVE ) ");
                sqlQuery.AppendLine($" VALUES ({GetNewId()}, @USERNAME, @PASSWORD, 1 ) ");               

                Framework.Database.Transaction.ExecuteCreateObjectCommand(sqlQuery.ToString(), new string[] 
                { "@USERNAME", this.Username.ToString(), 
                    "@PASSWORD", this.Password.ToString() 
                });
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
                sqlQuery.AppendLine($" UPDATE [DBO].[Account] SET FIRST_NAME = @FIRST_NAME, LAST_NAME = @LAST_NAME, USERNAME = @USERNAME, ACCOUNT_PASSWORD = @ACCOUNT_PASSWORD ");
                sqlQuery.AppendLine($" WHERE ID_ACCOUNT = @ID_ACCOUNT ");
                Framework.Database.Transaction.ExecuteUpdateObjectCommand(sqlQuery.ToString(), new string[]
                {
                    "@FIRST_NAME", this.First_Name,
                    "@LAST_NAME", this.Last_Name,
                    "@USERNAME", this.Username,
                    "@ACCOUNT_PASSWORD", this.Password,
                    "@ID_ACCOUNT", this.IdAccount.ToString()
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load(string username, string password)
        {
            try
            {
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine(" SELECT ");
                sqlQuery.AppendLine(" ID_ACCOUNT, ");
                sqlQuery.AppendLine(" FIRST_NAME, ");
                sqlQuery.AppendLine(" LAST_NAME, ");
                sqlQuery.AppendLine(" USERNAME, ");
                sqlQuery.AppendLine(" ACCOUNT_PASSWORD, ");
                sqlQuery.AppendLine(" ACTIVE ");
                sqlQuery.AppendLine(" FROM [DBO].[ACCOUNT](NOLOCK) ");
                sqlQuery.AppendLine(" WHERE  USERNAME = @USERNAME AND ACCOUNT_PASSWORD = @ACCOUNT_PASSWORD ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString(), new string[] 
                {
                    "@USERNAME", username,
                    "@ACCOUNT_PASSWORD", password
                });

                if (obj == null)
                {
                    this.IdAccount = -1;
                    this.Username = "";
                    this.First_Name = "";
                    this.Last_Name = "";
                    this.Password = "";
                    this.Active = false;
                    return;
                }
                else
                {
                    this.IdAccount = obj.Field<int>("ID_ACCOUNT");
                    this.Username = obj.Field<string>("USERNAME");
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
                sqlQuery.AppendLine(" SELECT ");
                sqlQuery.AppendLine(" ID_ACCOUNT, ");
                sqlQuery.AppendLine(" FIRST_NAME, ");
                sqlQuery.AppendLine(" LAST_NAME, ");
                sqlQuery.AppendLine(" USERNAME, ");
                sqlQuery.AppendLine(" ACCOUNT_PASSWORD, ");
                sqlQuery.AppendLine(" ACTIVE ");
                sqlQuery.AppendLine(" FROM [DBO].[ACCOUNT](NOLOCK) ");
                sqlQuery.AppendLine(" WHERE ID_ACCOUNT = @ID_ACCOUNT ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString(), new string[] { "@ID_ACCOUNT", id.ToString() });
                if (obj == null)
                {
                    this.IdAccount = -1;
                    this.Username = "";
                    this.First_Name = "";
                    this.Last_Name = "";
                    this.Password = "";
                    this.Active = false;
                    return;
                }
                else
                {
                    this.IdAccount = obj.Field<int>("ID_ACCOUNT");
                    this.Username = obj.Field<string>("USERNAME");
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
                sqlQuery.AppendLine(" SELECT ");
                sqlQuery.AppendLine(" ID_ACCOUNT, ");
                sqlQuery.AppendLine(" FIRST_NAME, ");
                sqlQuery.AppendLine(" LAST_NAME, ");
                sqlQuery.AppendLine(" USERNAME, ");
                sqlQuery.AppendLine(" ACCOUNT_PASSWORD, ");
                sqlQuery.AppendLine(" ACTIVE ");
                sqlQuery.AppendLine(" FROM [DBO].[ACCOUNT](NOLOCK) ");
                sqlQuery.AppendLine(" WHERE USERNAME = @USERNAME ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString(), new string[] { "@USERNAME", userN });
                if (obj == null)
                {
                    this.IdAccount = -1;
                    this.Username = "";
                    this.First_Name = "";
                    this.Last_Name = "";
                    this.Password = "";
                    this.Active = false;
                    return;
                }
                else
                {
                    this.IdAccount = obj.Field<int>("ID_ACCOUNT");
                    this.Username = obj.Field<string>("USERNAME");
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

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString(), new string[] { });
            int rowId = obj.Field<int>("ID_ACCOUNT");
            return rowId;
        }
        #endregion
    }
}
