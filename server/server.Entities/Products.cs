using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using server.Model;
using System.Data;
using server.Data.Sql;

namespace server.Entities
{
    public class Products
    {
        ProductsQueries productsQueries = new ProductsQueries();

        public void ClearList()
        {
            MainManager.Instance.productsList.Clear();
        }      

        public void GetProductsFromDB()
        {
            ClearList();
            MainManager.Instance.productsList = ((List<Product>)productsQueries.ResetList());            
            //return MainManager.Instance.productsList;
        }

        public Product GetProductById(string id)
        {
            Product product = ((Product)productsQueries.GetProductFromDB(id));
            return product;
        }

        public void AddNewProduct(int ProductID, string ProductName, int SupplierID, int CategoryID, string QuantityPerUnit, decimal UnitPrice, Int16 UnitsInStock, Int16 UnitsOnOrder, Int16 ReorderLevel, bool Discontinued)
        {
            Product product = new Product
            {
                ProductID = ProductID,
                ProductName = ProductName,
                SupplierID = SupplierID,
                CategoryID = CategoryID,
                QuantityPerUnit = QuantityPerUnit,
                UnitPrice = UnitPrice,
                UnitsInStock = UnitsInStock,
                UnitsOnOrder = UnitsOnOrder,
                ReorderLevel = ReorderLevel,
                Discontinued = Discontinued
            };
            MainManager.Instance.productsList.Add(product);

            productsQueries.InsertProductToDB(ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued);       
        }

        public void UpdateProductById(string id, int ProductID, string ProductName, int SupplierID, int CategoryID, string QuantityPerUnit, decimal UnitPrice, Int16 UnitsInStock, Int16 UnitsOnOrder, Int16 ReorderLevel, bool Discontinued)
        {
            productsQueries.UpdateProductInDB(id, ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued);
            //MainManager.Instance.contactsList[int.Parse(id)]=new Contact { ContactID=int.Parse(id), Name=Name, Email=Email, Phone=Phone, Message=Message};
        }

        public Product GetProductFromList(string id)
        {
            return MainManager.Instance.productsList[int.Parse(id)];
        }

        public void DeleteProductById(string id)
        {
            /*if (MainManager.Instance.productsList.Count == 0)
            {

            }
            else
            {
                MainManager.Instance.contactsList.RemoveAt(int.Parse(id));*/
            productsQueries.DeleteProductFromDB(id);
            //}
        }
    }
}
