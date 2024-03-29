﻿using System;
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
        public List<Product> productsList = new List<Product>();
        public List<Contact> contactsList = new List<Contact>();

        private MainManager() { }
        private static readonly MainManager _instance = new MainManager();
        public static MainManager Instance { get { return _instance; } }

        public Products products = new Products();
        public Contacts contacts = new Contacts();

        public void Init()
        {
            products.GetProductsFromDB();
        }
        public void Init2()
        {
            contacts.GetContactsFromDB();
        }
    }
}
