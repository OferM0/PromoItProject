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
using Utilities;

namespace server.Entities
{
    public class Contacts : BaseEntity
    {
        public Contacts(Logger log) : base(log) { contactsQueries = new ContactsQueries(base._log); }

        ContactsQueries contactsQueries;

        public void ClearList()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute  ClearList function in Contacts Entity." });
                MainManager.Instance.contactsList.Clear();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }
        
        public void GetContactsFromDB()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetContactsFromDB function in Contacts Entity." });
                ClearList();
                MainManager.Instance.contactsList = ((List<Contact>)contactsQueries.ResetList());
                //return MainManager.Instance.contactsList;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public Contact GetContactById(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetContactById(id:{id}) function in Contacts Entity." });
                Contact contact = ((Contact)contactsQueries.GetContactFromDB(id));
                return contact;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }          
        }

        public void AddNewContact(string Name, string Email, string Phone, string Message)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewContact function in Contacts Entity." });
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
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void UpdateContactById(string id, string Name, string Email, string Phone, string Message)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateContactById(id:{id}) function in Contacts Entity." });
                contactsQueries.UpdateContactInDB(id, Name, Email, Phone, Message);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public Contact GetContactFromList(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetContactFromList(id:{id}) function in Contacts Entity." });
                return MainManager.Instance.contactsList[int.Parse(id)];
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }     
        }
        
        public void DeleteContactById(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteContactById(id:{id}) function in Contacts Entity." });
                contactsQueries.DeleteContactFromDB(id);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }
    }
}
