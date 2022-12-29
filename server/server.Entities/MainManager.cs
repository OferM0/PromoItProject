using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace server.Entities
{
    public class MainManager
    {
        public List<Contact> contactsList = new List<Contact>();
        public Dictionary<string, User> usersList = new Dictionary<string, User>();
        public Dictionary<string, Activist> activistsList = new Dictionary<string, Activist>();
        public Dictionary<string, Organization> organizationsList = new Dictionary<string, Organization>();
        public Dictionary<string, Company> companiesList = new Dictionary<string, Company>();
        public List<Campaign> campaignsList = new List<Campaign>();
        public List<Product> productsList = new List<Product>();


        private MainManager() { }
        private static readonly MainManager _instance = new MainManager();
        public static MainManager Instance { get { return _instance; } }

        public Products products = new Products();
        public Contacts contacts = new Contacts();
        public Users users = new Users();
        public Activists activists = new Activists();
        public Organizations organizations = new Organizations();
        public Companies companies = new Companies();
        public Campaigns campaigns = new Campaigns();

        public void Init()
        {
            products.GetProductsFromDB();
        }
        public void Init2()
        {
            contacts.GetContactsFromDB();
        }
        public void Init3()
        {
            users.GetUsersFromDB();
        }
        public void Init4()
        {
            activists.GetActivistsFromDB();
        }
        public void Init5()
        {
            organizations.GetOrganizationsFromDB();
        }
        public void Init6()
        {
            companies.GetCompaniesFromDB();
        }
        public void Init7()
        {
            campaigns.GetCampaignsFromDB();
        }
    }
}
