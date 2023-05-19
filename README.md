# toy-land-api 
This is an API project made with C#. This solution was developed as a background API for an application made using the framework React Js that consumes it on the front end. It is a product-selling web site.

The main controller implemented returns product information based on a SQL Server database that was created.

## Database

The scripts I made for this database are in a specific folder if you want to recreate it, access [here](https://github.com/AaronCrvl/toy-land-api/tree/main/ConsoleToyLand/Script).

## Model

```C#
public class ProductModel
    {        
        public string idProduct;
        public string productName;
        public string shortDescription;
        public string imageUrl;
    }
```

## Current Methods
- GetProductById
```C#
  [HttpGet]    
  //https://localhost:44393/Product/GetProductById/
  public ActionResult GetProductById(int id)
```
- GetAllProducts
```C#
  [HttpGet]          
  //https://localhost:44393/Product/GetAllProducts/
  public ActionResult GetAllProducts()
```

- GetProductsByRegisterQuantity
```C#
  [HttpGet]          
  //https://localhost:44393/Product/GetProductsByRegisterQuantity/
  public ActionResult GetProductsByRegisterQuantity(int registers = -1)
```

## Front End Application
[![Dev.to](https://github-readme-stats.vercel.app/api/pin/?username=AaronCrvl&repo=toy-land-web&theme=dracula)](https://github.com/thepracticaldev/dev.to)
