using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data.Lists
{
    public class ListClientCart
    {
        public List<ClientCart> list;
        public List<Product> productList;
        public ListClientCart()
        {
            list = new List<ClientCart>();
            productList = new List<Product>();
        }

        #region Methods
        public List<Product> LoadProductList(int idAcct)
        {
            try
            {
                var data = new DataTable();                
                var list = new List<ClientCart>();

                StringBuilder query = new StringBuilder();
                query.AppendLine(" SELECT ");
                query.AppendLine(" ID_ACCT, ");
                query.AppendLine(" ID_PRODUCT ");
                query.AppendLine(" FROM [dbo].[ClientCart](NOLOCK) ");
                query.AppendLine(" WHERE ID_ACCT = @ID_ACCT ");

                data = Framework.Database.Transaction.ExecuteSelectListOfObjectCommand(query.ToString(),
                    new string[] { "@ID_ACCT", idAcct.ToString() }).Tables[0];

                foreach (DataRow product in data.Rows)
                {
                    var objProduct = new Product(product.Field<int>("ID_PRODUCT"));                                                            
                    productList.Add(objProduct);
                }
                return productList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
    #endregion
}
