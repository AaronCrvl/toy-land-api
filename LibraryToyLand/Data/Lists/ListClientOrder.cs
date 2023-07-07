using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data.Lists
{
    public class ListClientOrder
    {
        public List<Client_Order> list;
        public ListClientOrder()
        {
            list = new List<Client_Order>();
        }

        #region Methods
        public List<Client_Order> LoadClientOrderList(long idAccount)
        {
            try
            {
                var data = new DataTable();                
                var list = new List<Client_Order>();
                var sql = new StringBuilder();
                sql.AppendLine(" SELECT ID_CLIENT_ORDER, ID_ACCOUNT, ID_PRODUCT, FINISHED FROM [DBO].[ClientOrder](nolock) ");
                sql.AppendLine($" WHERE ID_ACCOUNT = {idAccount}");

                data = Framework.Database.Transaction.ExecuteSelectListOfObjectCommand(sql.ToString(), new string[] { }).Tables[0];
                foreach (DataRow clientOrder in data.Rows)
                {
                    var objClientOrder = new Client_Order();
                    objClientOrder.idClientOrder = clientOrder.Field<int>("ID_CLIENT_ORDER");
                    objClientOrder.idAccount = clientOrder.Field<int>("ID_ACCOUNT");
                    objClientOrder.idProduct = clientOrder.Field<int>("ID_PRODUCT");
                    objClientOrder.finished = clientOrder.Field<bool>("FINISHED");
                    list.Add(objClientOrder);
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
