﻿using server.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Data.Sql
{
    public class UsersQueries
    {
        public Dictionary<string, User> BuildUsersList(SqlDataReader reader)
        {
            Dictionary<string, User> usersList = new Dictionary<string, User>();

            while (reader.Read())
            {
                User user = new User();
                user.UserID = reader.GetString(reader.GetOrdinal("UserID"));
                user.Role = reader.GetString(reader.GetOrdinal("Role"));
                user.Name = reader.GetString(reader.GetOrdinal("Name"));
                user.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                user.Address = reader.GetString(reader.GetOrdinal("Address"));
                user.Url = reader.GetString(reader.GetOrdinal("Url"));
                user.Status = reader.GetDecimal(reader.GetOrdinal("Status"));
                user.TwitterHandle = reader.GetString(reader.GetOrdinal("TwitterHandle"));
                user.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")).ToString("yyyy-MM-dd");
                usersList.Add(user.UserID, user);
            }
            return usersList;
        }
        public User BuildUser(SqlDataReader reader)
        {
            User user = new User();

            while (reader.Read())
            {
                user.UserID = reader.GetString(reader.GetOrdinal("UserID"));                
                user.Role = reader.GetString(reader.GetOrdinal("Role"));
                user.Name = reader.GetString(reader.GetOrdinal("Name"));
                user.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                user.Address = reader.GetString(reader.GetOrdinal("Address"));
                user.Url = reader.GetString(reader.GetOrdinal("Url"));
                user.Status = reader.GetDecimal(reader.GetOrdinal("Status"));
                user.TwitterHandle = reader.GetString(reader.GetOrdinal("TwitterHandle"));
                user.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")).ToString("yyyy-MM-dd");
            }
            return user;
        }
        public object ResetList()
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult("select * from Users", BuildUsersList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void InsertUserToDB(string UserID, string Role, string Name, string Address, string Phone, string Url, decimal Status, string TwitterHandle, string CreateDate)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Users(UserID, Role, Name, Address, Phone, Url, Status, TwitterHandle, CreateDate) Values('{UserID}','{Role}','{Name}','{Address}','{Phone}','{Url}','{Status}','{TwitterHandle}','{CreateDate}')");
            }
            catch (Exception ex)
            {
            }
        }
        public object GetUserFromDB(string UserID)
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult($"Select * from Users where UserID= '{UserID}'", BuildUser);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteUserFromDB(string UserID)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Users where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateUserInDB(string UserID, string Name, string Address, string Phone, string Url, decimal Status)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Update Users set Name='{Name}' , Address='{Address}' , Phone='{Phone}' , Url='{Url}' , Status='{Status}' where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
