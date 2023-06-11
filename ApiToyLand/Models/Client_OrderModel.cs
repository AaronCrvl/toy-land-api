using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiToyLand.Models
{
    public class Client_OrderModel
    {
        public int idClientOrder;
        public int idAccount;        
        public int idProduct;
        public int idStatus;
        public bool finished;
        public string productName;
        public string statusDetail;        
        public string orderHashCode;
    }
}