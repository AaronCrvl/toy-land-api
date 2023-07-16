using ApiToyLand.Models;
using ApiToyLand.Repository.Interfaces;
using LibraryToyLand.Data.Lists;
using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiToyLand.Repository
{
    public class ClientCartRepository : IClientCartRepository
    {
        #region Methods
        public bool AddToCart(int idAccount, int idProduct)
        {
            return ClientCart.AddProductToCart(idAccount, idProduct);
        }

        public int GetCartCount(int idAccount)
        {
            return ClientCart.GetCartCount(idAccount);
        }

        public List<Product> GetCartProducts(int idAccount)
        {
            var list = new ListClientCart().LoadProductList(idAccount);
            return list;
        }

        public bool RemoveFromCart(int idAccount, int idProduct)
        {
            return ClientCart.RemoveProductFromCart(idAccount, idProduct);
        }
        #endregion        
    }
}