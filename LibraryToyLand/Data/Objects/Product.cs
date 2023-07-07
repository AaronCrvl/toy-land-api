using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data.Objects
{
    public class Product
    {
        #region Private Variables
        private long ID_PRODUCT;
        private string PRODUCT_NAME;
        private string SHORT_DESCRIPTION;
        private string IMAGE_URL;
        #endregion

        #region Properties
        public long IdProduct
        {
            get
            {
                return this.ID_PRODUCT;
            }
            set
            {
                this.ID_PRODUCT = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this.PRODUCT_NAME;
            }
            set
            {
                this.PRODUCT_NAME = value;
            }
        }

        public string ShortDescription
        {
            get
            {
                return this.SHORT_DESCRIPTION;
            }
            set
            {
                this.SHORT_DESCRIPTION = value;
            }
        }
        public string ImageUrl
        {
            get
            {
                return this.IMAGE_URL;
            }
            set
            {
                this.IMAGE_URL = value;
            }
        }
        #endregion

        #region Constructors
        public Product() 
        {
            IdProduct = -1;
            ProductName = "";
            ShortDescription = "";
            ImageUrl = "";
        }
        public Product(long pId)
        {
            if (pId > 0)
            {
                IdProduct = pId;
                Load();
            }            
        }
        #endregion

        #region Methods
        public void Load()
        {            
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendLine(" SELECT ");
            sqlQuery.AppendLine(" ID_PRODUCT, ");
            sqlQuery.AppendLine(" PRODUCT_NAME, ");
            sqlQuery.AppendLine(" SHORT_DESCRIPTION, ");
            sqlQuery.AppendLine(" IMAGE_URL ");
            sqlQuery.AppendLine(" FROM [DBO].[PRODUCT](NOLOCK) ");
            sqlQuery.AppendLine($" WHERE ID_PRODUCT = @ID_PRODUCT ");
            
            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString(), new string[] { "@ID_PRODUCT", IdProduct.ToString() });
            if (obj == null)
            {
                this.IdProduct = -1;
                this.ProductName = string.Empty;
                this.ShortDescription = string.Empty;
                this.ImageUrl = string.Empty;
            }
            else
            {
                this.IdProduct = obj.Field<int>("ID_PRODUCT");
                this.ProductName = obj.Field<string>("PRODUCT_NAME");
                this.ShortDescription = obj.Field<string>("SHORT_DESCRIPTION");
                this.ImageUrl = obj.Field<string>("IMAGE_URL");
            }            
        }
        #endregion
    }
}
