using server.Data.Sql;
using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Entities
{
    public class Activists
    {
        ActivistsQueries activistsQueries = new ActivistsQueries();

        public void ClearList()
        {
            MainManager.Instance.activistsList.Clear();
        }

        public void GetActivistsFromDB()
        {
            ClearList();
            MainManager.Instance.activistsList = ((Dictionary<string, Activist>)activistsQueries.ResetList());
            //return MainManager.Instance.activistsList;
        }

        public Activist GetActivistById(string UserID)
        {
            Activist activist = ((Activist)activistsQueries.GetActivistFromDB(UserID));
            return activist;
        }

        public void AddNewActivist(string UserID, string Name, string Address, string Phone, decimal Money)
        {
            Activist activist = new Activist
            {
                UserID = UserID,
                Name = Name,
                Address = Address,
                Phone = Phone,
                Money = Money,
            };
            MainManager.Instance.activistsList.Add(UserID, activist);

            activistsQueries.InsertActivistToDB(UserID, Name, Address, Phone, Money);
        }

        public void UpdateActivistById(string UserID, string Name, string Address, string Phone, decimal Money)  //not update to Role
        {
            activistsQueries.UpdateActivistInDB(UserID, Name, Address, Phone, Money);
            //MainManager.Instance.activistsList[ActivistID]=new Activist { ActivistID=ActivistID, Role = Role, Name = Name, Address = Address, Phone = Phone, Url = Url};
        }

        public Activist GetActivistFromList(string UserID)
        {
            return MainManager.Instance.activistsList[UserID];
        }

        public void DeleteActivistById(string UserID)
        {
            /*if (MainManager.Instance.activistsList.Count == 0)
            {

            }
            else
            {
                MainManager.Instance.activistsList.RemoveAt(ActivistID);*/
            activistsQueries.DeleteActivistFromDB(UserID);
            //}
        }
    }
}
