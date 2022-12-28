using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using server.Data.Sql;

namespace server.Entities
{
    public class Users
    {
        UsersQueries usersQueries = new UsersQueries();

        public void ClearList()
        {
            MainManager.Instance.usersList.Clear();
        }

        public void GetUsersFromDB()
        {
            ClearList();
            MainManager.Instance.usersList = ((Dictionary<string, User>)usersQueries.ResetList());
            //return MainManager.Instance.usersList;
        }

        public User GetUserById(string UserID)
        {
            User user = ((User)usersQueries.GetUserFromDB(UserID));
            return user;
        }

        public void AddNewUser(string UserID, string Role, string Name, string Address, string Phone, string Url)
        {
            User user = new User
            {
                UserID = UserID,
                Role = Role,
                Name = Name,
                Address = Address,
                Phone = Phone,
                Url= Url
            };
            MainManager.Instance.usersList.Add(UserID,user);

            usersQueries.InsertUserToDB(UserID, Role, Name,  Address, Phone, Url);
        }

        public void UpdateUserById(string UserID, string Name, string Address, string Phone, string Url)  //not update to Role
        {
            usersQueries.UpdateUserInDB(UserID, Name, Address, Phone, Url);
            //MainManager.Instance.usersList[UserID]=new User { UserID=UserID, Role = Role, Name = Name, Address = Address, Phone = Phone, Url = Url};
        }

        public User GetUserFromList(string UserID)
        {
            return MainManager.Instance.usersList[UserID];
        }

        public void DeleteUserById(string UserID)
        {
            /*if (MainManager.Instance.usersList.Count == 0)
            {

            }
            else
            {
                MainManager.Instance.usersList.RemoveAt(UserID);*/
            usersQueries.DeleteUserFromDB(UserID);
            //}
        }
    }
}
