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
        }
        public Account(long key) {
            this.TxtKey = key;
        }
        #endregion

        #region Variables
        public long IdAccount;
        public long TxtKey;
        public DateTime? CreationDate;
        public DateTime? ExpireDate;
        #endregion

        #region Methods
        public void Load()
        {
            //var sqlQuery = new StringBuilder();
            //sqlQuery.AppendLine(" SELECT ID_PRODUCT, PRODUCT_NAME, SHORT_DESCRIPTION FROM [DBO].[PRODUCT](NOLOCK) ");
            //sqlQuery.AppendLine($" WHERE ID_PRODUCT = {IdProduct} ");

            //var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
            //if (obj == null)
            //{
            //    this.IdProduct = -1;
            //    this.ProductName = string.Empty;
            //    this.ShortDescription = string.Empty;
            //}
            //else
            //{
            //    this.IdProduct = obj.Field<int>("ID_PRODUCT");
            //    this.ProductName = obj.Field<string>("PRODUCT_NAME");
            //    this.ShortDescription = obj.Field<string>("SHORT_DESCRIPTION");
            //}
        }

        public void LoadByKey(long key)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine(" SELECT ID_ACCOUNT, TXT_KEY, CREATION_DATE, EXPIRE_DATE FROM [DBO].[ACCOUNT](NOLOCK) ");
            sqlQuery.AppendLine($" WHERE TXT_KEY = {key} ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
            if (obj == null)
            {
                this.IdAccount = -1;
                this.TxtKey = -1;
                this.CreationDate = null;
                this.ExpireDate = null;
                return;
            }
            else
            {
                this.IdAccount = obj.Field<int>("ID_ACCOUNT");
                this.TxtKey = obj.Field<int>("TXT_KEY");
                this.CreationDate = obj.Field<DateTime?>("CREATION_DATE");
                this.ExpireDate = obj.Field<DateTime?>("EXPIRE_DATE");
            }
        }
        #endregion
    }
}
