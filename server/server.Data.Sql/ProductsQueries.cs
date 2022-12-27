using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;

namespace server.Data.Sql
{
    public class ProductsQueries
    {
        public List<Product> BuildProductsList(SqlDataReader reader)
        {
            List<Product> productsList = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product();
                product.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                product.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                product.SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID"));
                product.CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID"));
                product.QuantityPerUnit = reader.GetString(reader.GetOrdinal("QuantityPerUnit"));
                product.UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"));
                product.UnitsInStock = reader.GetInt16(reader.GetOrdinal("UnitsInStock"));
                product.UnitsOnOrder = reader.GetInt16(reader.GetOrdinal("UnitsOnOrder"));
                product.ReorderLevel = reader.GetInt16(reader.GetOrdinal("ReorderLevel"));
                product.Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued"));
                productsList.Add(product);
            }
            return productsList;
        }
        public Product BuildProduct(SqlDataReader reader)
        {
            Product product = new Product();

            while (reader.Read())
            {
                product.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                product.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                product.SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID"));
                product.CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID"));
                product.QuantityPerUnit = reader.GetString(reader.GetOrdinal("QuantityPerUnit"));
                product.UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"));
                product.UnitsInStock = reader.GetInt16(reader.GetOrdinal("UnitsInStock"));
                product.UnitsOnOrder = reader.GetInt16(reader.GetOrdinal("UnitsOnOrder"));
                product.ReorderLevel = reader.GetInt16(reader.GetOrdinal("ReorderLevel"));
                product.Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued"));
            }
            return product;
        }

        public object ResetList()
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult("select * from Products", BuildProductsList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void InsertProductToDB(int ProductID, string ProductName, int SupplierID, int CategoryID, string QuantityPerUnit, decimal UnitPrice, Int16 UnitsInStock, Int16 UnitsOnOrder, Int16 ReorderLevel, bool Discontinued)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Products(ProductID, ProductName, SupplierID, CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued) Values({ProductID},'{ProductName}',{SupplierID},'{CategoryID}'),'{QuantityPerUnit}',{UnitPrice},'{UnitsInStock}'),'{UnitsOnOrder}',{ReorderLevel},'{Discontinued}')");
            }
            catch (Exception ex)
            {
            }
        }

        public object GetProductFromDB(string id)
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult($"select * from Products where ProductID= '{id}'", BuildProduct);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteProductFromDB(string id)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Products where ProductID= '{id}'");
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateProductInDB(string id, int ProductID, string ProductName, int SupplierID, int CategoryID, string QuantityPerUnit, decimal UnitPrice, Int16 UnitsInStock, Int16 UnitsOnOrder, Int16 ReorderLevel, bool Discontinued)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Update Products set ProductID='{ProductID}' , ProductName='{ProductName}' , SupplierID='{SupplierID}' , CategoryID='{CategoryID}', QuantityPerUnit='{QuantityPerUnit}' , UnitPrice='{UnitPrice}' , UnitsInStock='{UnitsInStock}' , UnitsOnOrder='{UnitsOnOrder}' , ReorderLevel='{ReorderLevel}' , Discontinued='{Discontinued}' where ProductID= '{id}'");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
