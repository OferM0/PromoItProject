using server.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data;
using server.Data.Sql;

namespace server.Entities
{
    public class Contacts
    {
        ContactsQueries contactsQueries = new ContactsQueries();

        public void ClearList()
        {
            MainManager.Instance.contactsList.Clear();
        }
        
        public void GetContactsFromDB()
        {
            ClearList();
            MainManager.Instance.contactsList = ((List<Contact>) contactsQueries.ResetList());           
            //return MainManager.Instance.contactsList;
        }

        public Contact GetContactById(string id)
        {
            Contact contact = ((Contact)contactsQueries.GetContactFromDB(id));
            return contact;
        }

        public void AddNewContact(string Name, string Email, string Phone, string Message)
        {
            Contact contact = new Contact
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Message = Message
            };
            MainManager.Instance.contactsList.Add(contact);

            contactsQueries.InsertContactToDB(Name, Email, Phone, Message);
        }

        public void UpdateContactById(string id, string Name, string Email, string Phone, string Message)
        {
            contactsQueries.UpdateContactInDB(id, Name, Email, Phone, Message);
            //MainManager.Instance.contactsList[int.Parse(id)]=new Contact { ContactID=int.Parse(id), Name=Name, Email=Email, Phone=Phone, Message=Message};
        }

        public Contact GetContactFromList(string id)
        {
            return MainManager.Instance.contactsList[int.Parse(id)];
        }
        
        public void DeleteContactById(string id)
        {
            /*if (MainManager.Instance.contactsList.Count == 0)
            {

            }
            else
            {
                MainManager.Instance.contactsList.RemoveAt(int.Parse(id));*/
                contactsQueries.DeleteContactFromDB(id);
            //}
        }
    }
}
