using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data.Objects
{
    public class Client_Order
    {        
        public int idClientOrder;
        public int idAccount;
        public int idProduct;
        public bool finished;

        public Client_Order() 
        {
            idClientOrder = -1;
            idAccount = -1;
            idProduct = -1;
            finished = false;
        }

        public void Load(int idClientOrder)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine(" ID_CLIENT_ORDER, ");
            sql.AppendLine(" ID_ACCOUNT, ");
            sql.AppendLine(" ID_PRODUCT, ");
            sql.AppendLine(" FINISHED ");
            sql.AppendLine(" FROM [DBO].[ClientOrder] ");
            sql.AppendLine(" WHERE ID_CLIENT_ORDER = @ID_CLIENT_ORDER ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sql.ToString(), new string[] { "@ID_CLIENT_ORDER", idClientOrder.ToString() });
            if (obj == null)
            {
                this.idClientOrder = -1;
                this.idAccount = -1;
                this.idProduct = -1;
                this.finished = false;
            }
            else
            {
                this.idClientOrder = obj.Field<int>("ID_CLIENT_ORDER");
                this.idAccount = obj.Field<int>("ID_ACCOUNT");
                this.idProduct = obj.Field<int>("ID_PRODUCT");
                this.finished = obj.Field<bool>("FINISHED");
            }
        }

        public void LoadByAccountAndProduct(int idAccount, long idProduct)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine(" ID_CLIENT_ORDER, ");
            sql.AppendLine(" ID_ACCOUNT, ");
            sql.AppendLine(" ID_PRODUCT, ");
            sql.AppendLine(" FINISHED ");
            sql.AppendLine(" FROM [DBO].[ClientOrder] ");
            sql.AppendLine($" WHERE ID_ACCOUNT = @ID_ACCOUNT AND ID_PRODUCT = @ID_PRODUCT ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sql.ToString(), new string[] 
            { 
                "@ID_ACCOUNT", idAccount.ToString(),
                "@ID_PRODUCT", idProduct.ToString()
            });

            if (obj == null)
            {
                this.idClientOrder = -1;
                this.idAccount = -1;
                this.idProduct = -1;
                this.finished = false;
            }
            else
            {
                this.idClientOrder = obj.Field<int>("ID_CLIENT_ORDER");
                this.idAccount = obj.Field<int>("ID_ACCOUNT");
                this.idProduct = obj.Field<int>("ID_PRODUCT");
                this.finished = obj.Field<bool>("FINISHED");
            }
        }

        public int Save()
        {
            var idClientOrder = GetNewId();
            var sql = new StringBuilder();
            sql.AppendLine(" INSERT INTO [DBO].[ClientOrder] ");
            sql.AppendLine(" ( ID_CLIENT_ORDER, ");
            sql.AppendLine(" ID_ACCOUNT, ");
            sql.AppendLine(" ID_PRODUCT, ");
            sql.AppendLine(" FINISHED ) ");            
            sql.AppendLine($" VALUES ({idClientOrder}, @ID_ACCOUNT, @ID_PRODUCT, 0 )");

            Framework.Database.Transaction.ExecuteCreateObjectCommand(sql.ToString(), new string[] 
            { "@ID_ACCOUNT", this.idAccount.ToString(),
                "@ID_PRODUCT", this.idProduct.ToString()
            });

            return idClientOrder;
        }

        private int GetNewId()
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine(" SELECT (ID_CLIENT_ORDER+1) AS ID_CLIENT_ORDER FROM [DBO].[ClientOrder](NOLOCK) ");
            sqlQuery.AppendLine($" ORDER BY ID_CLIENT_ORDER DESC ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString(), new string[] { });
            int rowId = obj == null ? 1 : obj.Field<int>("ID_CLIENT_ORDER");

            if (rowId <= 0)
                return 1;
            else
                return rowId;
        }

    }                               
}                                    
