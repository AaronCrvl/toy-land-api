using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data.Lists
{
    public class ListProducts
    {
        public List<Product> list;
        public ListProducts()
        {
            list = new List<Product>();
        }

        #region Methods
        public List<Product> LoadProductList()
        {
            try
            {
                var data = new DataTable();
                var sqlQuery = new StringBuilder();
                var list = new List<Product>();

                sqlQuery.AppendLine(" SELECT ID_PRODUCT, PRODUCT_NAME, SHORT_DESCRIPTION, IMAGE_URL FROM [dbo].[Product] ORDER BY PRODUCT_NAME; ");
                data = Framework.Database.Transaction.ExecuteSelectListOfObjectCommand(sqlQuery.ToString(), new string[] { }).Tables[0];
                foreach (DataRow product in data.Rows)
                {
                    var objProduct = new Product();
                    objProduct.IdProduct = product.Field<int>("ID_PRODUCT");
                    objProduct.ProductName = product.Field<string>("PRODUCT_NAME");
                    objProduct.ShortDescription = product.Field<string>("SHORT_DESCRIPTION");
                    objProduct.ImageUrl = product.Field<string>("IMAGE_URL");
                    list.Add(objProduct);
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
