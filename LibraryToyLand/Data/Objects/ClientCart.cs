using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data.Objects
{
    public class ClientCart
    {
        #region Constructors
        public ClientCart()
        {
            this.idAccount = -1;
            this.idProduct = -1;
        }

        public ClientCart(int idAcct)
        {
            this.idAccount = idAcct;
            this.idProduct = -1;
        }

        public ClientCart(int idAcct, int idProduct)
        {
            this.idAccount = idAcct;
            this.idProduct = idProduct;
        }
        #endregion        

        #region Variables
        public int idAccount;
        public int idProduct;
        #endregion

        #region Methods        
        public bool Validate() 
        {
            if (idAccount > 0 && idProduct > 0)
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine(" SELECT ");
                query.AppendLine(" ID_ACCT, ");
                query.AppendLine(" ID_PRODUCT, ");
                query.AppendLine(" FROM [dbo].[ClientCart](NOLOCK) ");
                query.AppendLine(" WHERE ID_ACCT = @ID_ACCT AND ID_PRODUCT = @ID_PRODUCT ");

                Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(query.ToString(), new string[]
                {
                    "@ID_ACCT", idAccount.ToString(),
                    "@ID_PRODUCT", idProduct.ToString()
                });
            }

            return false;
        }

        public static int GetCartCount(int idAcct)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine(" SELECT COUNT(*) AS QTD ");                        
            query.AppendLine(" FROM [dbo].[ClientCart](NOLOCK) ");
            query.AppendLine(" WHERE ID_ACCT = @ID_ACCT ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(query.ToString(), 
                new string[] { "@ID_ACCT", idAcct.ToString() });

            if (obj == null)
                return 0;
            else 
            {
                int count = obj.Field<int>("QTD");
                return count;
            }
        }

        public static bool AddProductToCart(int idAcct, int idProduct)
        {
            bool execute = false;
            if (idAcct > 0 && idProduct > 0)
            {
                StringBuilder _query = new StringBuilder();
                _query.AppendLine(" SELECT ");
                _query.AppendLine(" ID_ACCT, ");
                _query.AppendLine(" ID_PRODUCT ");
                _query.AppendLine(" FROM [dbo].[ClientCart](NOLOCK) ");
                _query.AppendLine(" WHERE ID_ACCT = @ID_ACCT AND ID_PRODUCT = @ID_PRODUCT ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(_query.ToString(), new string[]
                {
                    "@ID_ACCT", idAcct.ToString(),
                    "@ID_PRODUCT", idProduct.ToString()
                });

                if (obj == null)
                    execute = true;

                if (execute)
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" INSERT INTO [dbo].[ClientCart] (ID_ACCT, ID_PRODUCT) ");
                    query.AppendLine(" VALUES (@ID_ACCT, @ID_PRODUCT) ");

                    Framework.Database.Transaction.ExecuteCreateObjectCommand(query.ToString(), new string[]
                    {
                        "@ID_ACCT", idAcct.ToString(),
                        "@ID_PRODUCT", idProduct.ToString()
                    });

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public static bool RemoveProductFromCart(int idAcct, int idProduct)
        {
            bool execute = false;
            if (idAcct > 0 && idProduct > 0)
            {
                StringBuilder _query = new StringBuilder();
                _query.AppendLine(" SELECT ");
                _query.AppendLine(" ID_ACCT, ");
                _query.AppendLine(" ID_PRODUCT ");
                _query.AppendLine(" FROM [dbo].[ClientCart](NOLOCK) ");
                _query.AppendLine(" WHERE ID_ACCT = @ID_ACCT AND ID_PRODUCT = @ID_PRODUCT ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(_query.ToString(), new string[]
                {
                    "@ID_ACCT", idAcct.ToString(),
                    "@ID_PRODUCT", idProduct.ToString()
                });

                if (obj != null)
                    execute = true;

                if (execute)
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" DELETE FROM ClientCart ");
                    query.AppendLine(" WHERE ID_ACCT = @ID_ACCT AND ID_PRODUCT = @ID_PRODUCT ");

                    Framework.Database.Transaction.ExecuteDeleteSingleObjectCommand(query.ToString(), new string[]
                    {
                        "@ID_ACCT", idAcct.ToString(),
                        "@ID_PRODUCT", idProduct.ToString()
                    });

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        #endregion
    }
}
