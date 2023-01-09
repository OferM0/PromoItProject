using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace server.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string CompanyID { get; set; }
        public string OrganizationID { get; set; }
        public int CampaignID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ActivistID { get; set; }
        public bool DonatedByActivist { get; set; }
        public bool Shipped { get; set; }
        public string Image { get; set; }

    }
}
