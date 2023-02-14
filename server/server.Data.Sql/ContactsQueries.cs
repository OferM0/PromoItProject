using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;
using Utilities;

namespace server.Data.Sql
{
    public class ContactsQueries : BaseDataSql
    {
        public ContactsQueries(Logger log) : base(log) { }

        public List<Contact> BuildContactsList(SqlDataReader reader)
        {
            List<Contact> contactsList = new List<Contact>();

            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildContactsList function in ContactsQueries." });
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
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildContactsList function in ContactsQueries." });
                throw ex;
            }
        }
        public Contact BuildContact(SqlDataReader reader)
        {
            Contact contact = new Contact();
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildContact function in ContactsQueries." });
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
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildContact function in ContactsQueries." });
                throw ex;
            }
        }
        public object ResetList()
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute ResetList function in ContactsQueries." });
                return DAL.SqlQuery.RunCommandResult("select * from Contacts", BuildContactsList);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run ResetList function in ContactsQueries." });
                throw ex;
            }
        }
        public void InsertContactToDB(string Name, string Email, string Phone, string Message)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute InsertContactToDB function in ContactsQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Contacts(Name, Email, Phone, Message) Values('{Name}','{Email}','{Phone}','{Message}')");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InsertContactToDB function in ContactsQueries." });
                throw ex;
            }
        }
        public object GetContactFromDB(string id)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetContactFromDB(id:{id}) function in ContactsQueries." });
                return DAL.SqlQuery.RunCommandResult($"Select * from Contacts where ContactID= '{id}'", BuildContact);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetContactFromDB function in ContactsQueries." });
                throw ex;
            }
        }

        public void DeleteContactFromDB(string id)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteContactFromDB(id:{id}) function in ContactsQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Contacts where ContactID= '{id}'");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run DeleteContactFromDB function in ContactsQueries." });
                throw ex;
            }
        }

        public void UpdateContactInDB(string id, string Name, string Email, string Phone, string Message)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateContactInDB(id:{id}) function in ContactsQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Update Contacts set Name='{Name}' , Email='{Email}' , Phone='{Phone}' , Message='{Message}' where ContactID= '{id}'");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run UpdateContactInDB function in ContactsQueries." });
                throw ex;
            }
        }
    }
}
