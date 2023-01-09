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

        public void AddNewProduct(string Name, string Description, decimal Price, string ActivistID, string CompanyID, string OrganizationID, int CampaignID, bool DonatedByActivist, bool Shipped, string Image)
        {
            Product product = new Product
            {
                Name = Name,
                Description = Description,
                Price = Price,
                ActivistID = ActivistID,
                CompanyID = CompanyID,
                OrganizationID = OrganizationID,
                CampaignID = CampaignID,
                DonatedByActivist = DonatedByActivist,
                Shipped= Shipped,
                Image= Image
            };
            MainManager.Instance.productsList.Add(product);

            productsQueries.InsertProductToDB(Name, Description, Price, ActivistID, CompanyID, OrganizationID, CampaignID, DonatedByActivist, Shipped, Image);       
        }

        public void UpdateProductById(string Id, string Name, string Description, decimal Price, string ActivistID, string CompanyID, string OrganizationID, int CampaignID, bool DonatedByActivist, bool Shipped)
        {
            productsQueries.UpdateProductInDB(Id, Name, Description, Price, ActivistID, CompanyID, OrganizationID, CampaignID, DonatedByActivist, Shipped);
            //MainManager.Instance.productList[int.Parse(id)]=new Product { ProductID=int.Parse(id), Name=Name, Email=Email, Phone=Phone, Message=Message};
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
                MainManager.Instance.productsList.RemoveAt(int.Parse(id));*/
            productsQueries.DeleteProductFromDB(id);
            //}
        }
    }
}
