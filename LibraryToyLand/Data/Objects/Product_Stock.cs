﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Data.Objects
{
    public class Product_Stock
    {
        private int IdProduct;
        private int AvailableQtt;

        public int Id_Product {
            get {
                return this.IdProduct;
            }
            set
            {
                this.IdProduct = value;
            } 
        }

        public int Available_Qtt
        {
            get
            {
                return this.AvailableQtt;
            }
            set
            {
                this.AvailableQtt = value;
            }
        }

        public void Load(int Id)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine(" ID_PRODUCT, ");
            sql.AppendLine(" AVAILABLE_QNTT ");
            sql.AppendLine(" FROM [DBO].[Product_Stock](NOLOCK) ");
            sql.AppendLine($" WHERE ID_PRODUCT = @ID_PRODUCT ");

            var obj = Framework.Database.Transaction.ExecuteSelectSingleObjectCommand(sql.ToString(), new string[] { "@ID_PRODUCT", Id.ToString()});
            if (obj == null)
            {
                this.AvailableQtt = -1;
                this.IdProduct = -1;
                return;
            }
            else
            {
                this.AvailableQtt = obj.Field<int>("AVAILABLE_QNTT");
                this.IdProduct = obj.Field<int>("ID_PRODUCT");
                return;
            }
        }
    }
}
