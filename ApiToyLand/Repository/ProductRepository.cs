using ApiToyLand.Repository.Interfaces;
using LibraryToyLand.Data.Lists;
using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiToyLand.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAllProducts()
        {
            var list = new ListProducts();
            return list.LoadProductList();
        }
        public Product GetProductById(int id)
        {            
            return new Product(id);
        }
        public Product_Stock GetProductStock(int id)
        {
            var stock = new Product_Stock();
            stock.Load(id);
            return stock;
        }
        public string GetProductName(int id)
        {
            if (id > 0)            
                return new Product(id).ProductName;
            else
                return "Id Not Found";
        }
    }
}