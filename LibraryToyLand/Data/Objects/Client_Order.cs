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
            sql.AppendLine(" SELECT ID_CLIENT_ORDER, ID_ACCOUNT, ID_PRODUCT, FINISHED FROM [DBO].[ClientOrder] ");
            sql.AppendLine($" WHERE ID_CLIENT_ORDER = {idClientOrder} ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sql.ToString());
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

        public void Load(int idAccount, long idProduct)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT ID_CLIENT_ORDER, ID_ACCOUNT, ID_PRODUCT, FINISHED FROM [DBO].[ClientOrder] ");
            sql.AppendLine($" WHERE ID_ACCOUNT = {idAccount} AND ID_PRODUCT = {idProduct} ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sql.ToString());
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

        public void Save()
        {
            var sql = new StringBuilder();
            sql.AppendLine(" INSERT INTO [DBO].[ClientOrder] (ID_CLIENT_ORDER, ID_ACCOUNT, ID_PRODUCT, FINISHED) ");            
            sql.AppendLine($" VALUES ({GetNewId()}), {this.idAccount}, {this.idProduct}, {this.finished} ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sql.ToString());
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
        private int GetNewId()
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine(" SELECT (ID_CLIENT_ORDER+1) AS ID_CLIENT_ORDER FROM [DBO].[ClientOrder](NOLOCK) ");
            sqlQuery.AppendLine($" ORDER BY ID_CLIENT_ORDER DESC ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
            int rowId = obj.Field<int>("ID_CLIENT_ORDER");
            return rowId;
        }

    }                               
}                                    
