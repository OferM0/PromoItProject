using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;

namespace server.Data.Sql
{
    public class ContactsQueries
    {
        public List<Contact> BuildContactsList(SqlDataReader reader)
        {
            List<Contact> contactsList = new List<Contact>();

            while (reader.Read())
            {
                Contact contact = new Contact();
                contact.ContactID = reader.GetInt32(reader.GetOrdinal("ContactID"));
                contact.Name = reader.GetString(reader.GetOrdinal("Name"));
                contact.Email = reader.GetString(reader.GetOrdinal("Email"));
                contact.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                contact.Message = reader.GetString(reader.GetOrdinal("Message"));
                contactsList.Add(contact);
            }
            return contactsList;
        }
        public Contact BuildContact(SqlDataReader reader)
        {
            Contact contact = new Contact();

            while (reader.Read())
            {
                contact.ContactID = reader.GetInt32(reader.GetOrdinal("ContactID"));
                contact.Name = reader.GetString(reader.GetOrdinal("Name"));
                contact.Email = reader.GetString(reader.GetOrdinal("Email"));
                contact.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                contact.Message = reader.GetString(reader.GetOrdinal("Message"));
            }
            return contact;
        }
        public object ResetList()
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult("select * from Contacts", BuildContactsList);               
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void InsertContactToDB(string Name, string Email, string Phone, string Message)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Contacts(Name, Email, Phone, Message) Values('{Name}','{Email}','{Phone}','{Message}')");
            }
            catch (Exception ex)
            {
            }
        }
        public object GetContactFromDB(string id)
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult($"Select * from Contacts where ContactID= '{id}'", BuildContact);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteContactFromDB(string id)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Contacts where ContactID= '{id}'");
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateContactInDB(string id, string Name, string Email, string Phone, string Message)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Update Contacts set Name='{Name}' , Email='{Email}' , Phone='{Phone}' , Message='{Message}' where ContactID= '{id}'");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
