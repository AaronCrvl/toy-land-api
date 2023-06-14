﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data.Objects
{
    public class Product_Order
    {
        public Product_Order()
        {
            this.ID_PRODUCT_ORDER = -1;
            this.ID_CLIENT_ORDER = -1;
            this.ID_STATUS_ORDER = -1;
            this.PRODUCT_NAME = "";
            this.EMAIL = "";
            this.CLIENT_LOCATION = "";
            this.HASH_CODE = "";
        }

        #region Variables
        public int ID_PRODUCT_ORDER;
        public int ID_CLIENT_ORDER;
        public int ID_STATUS_ORDER;
        public string PRODUCT_NAME;
        public string EMAIL;
        public string CLIENT_LOCATION;
        public string HASH_CODE;
        #endregion

        #region Methods
        public void SaveNew()
        {
            try
            {
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine(" INSERT INTO [DBO].[ProductOrder] (ID_PRODUCT_ORDER, ID_CLIENT_ORDER, ID_STATUS_ORDER, PRODUCT_NAME, EMAIL, CLIENT_LOCATION, HASH_CODE) ");
                sqlQuery.AppendLine($" VALUES ({GetNewId()}, {this.ID_CLIENT_ORDER}, {this.ID_STATUS_ORDER}, '{this.PRODUCT_NAME}', '{this.EMAIL}', '{this.CLIENT_LOCATION}', '{this.HASH_CODE}') ");
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
                sqlQuery.AppendLine($" UPDATE [DBO].[ProductOrder] ");
                sqlQuery.AppendLine($" SET ID_STATUS_ORDER = {this.ID_STATUS_ORDER}, PRODUCT_NAME = '{this.PRODUCT_NAME}', EMAIL = '{this.EMAIL}', CLIENT_LOCATION = '{this.CLIENT_LOCATION}', HASH_CODE = '{this.HASH_CODE}'");
                sqlQuery.AppendLine($" WHERE ID_PRODUCT_ORDER = {this.ID_CLIENT_ORDER} ");
                Framework.Database.Transaction.ExecuteUpdateObjectCommand(sqlQuery.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load(long idClientOrder)
        {
            try
            {
                var sqlQuery = new StringBuilder();
                sqlQuery.AppendLine(" SELECT ID_PRODUCT_ORDER, ID_CLIENT_ORDER, ID_STATUS_ORDER, PRODUCT_NAME, EMAIL, CLIENT_LOCATION, HASH_CODE FROM [DBO].[ProductOrder](NOLOCK) ");
                sqlQuery.AppendLine($" WHERE  ID_CLIENT_ORDER = {idClientOrder} ");

                var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
                if (obj == null)
                {
                    this.ID_PRODUCT_ORDER = -1;
                    this.ID_CLIENT_ORDER = -1;
                    this.ID_STATUS_ORDER = -1;
                    this.PRODUCT_NAME = "";
                    this.EMAIL = "";
                    this.CLIENT_LOCATION = "";
                    this.HASH_CODE = "";
                    return;
                }
                else
                {
                    this.ID_PRODUCT_ORDER = obj.Field<int>("ID_PRODUCT_ORDER");
                    this.ID_CLIENT_ORDER = obj.Field<int>("ID_CLIENT_ORDER");
                    this.ID_STATUS_ORDER = obj.Field<int>("ID_STATUS_ORDER");
                    this.PRODUCT_NAME = obj.Field<string>("PRODUCT_NAME");
                    this.EMAIL = obj.Field<string>("EMAIL");
                    this.CLIENT_LOCATION = obj.Field<string>("CLIENT_LOCATION");
                    this.HASH_CODE = obj.Field<string>("HASH_CODE");
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
            sqlQuery.AppendLine(" SELECT (ID_PRODUCT_ORDER+1) AS ID_PRODUCT_ORDER FROM [DBO].[ProductOrder](NOLOCK) ");
            sqlQuery.AppendLine($" ORDER BY ID_PRODUCT_ORDER DESC ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sqlQuery.ToString());
            int rowId = obj == null ? 1 : obj.Field<int>("ID_PRODUCT_ORDER");

            if (rowId <= 0 )
                return 1;
            else
                return rowId;
        }
        #endregion
    }
}
