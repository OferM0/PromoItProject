using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class User
    {
        public string UserID { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public decimal Status { get; set; }
        public string TwitterHandle { get; set; }
    }
}
