using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using server.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Data.Sql
{
    public class ActivistsQueries
    {
        public Dictionary<string, Activist> BuildActivistsList(SqlDataReader reader)
        {
            Dictionary<string, Activist> activistsList = new Dictionary<string, Activist>();

            while (reader.Read())
            {
                Activist activist = new Activist();
                activist.UserID = reader.GetString(reader.GetOrdinal("UserID"));
                activist.Name = reader.GetString(reader.GetOrdinal("Name"));
                activist.Address = reader.GetString(reader.GetOrdinal("Address"));
                activist.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                activist.Money = reader.GetDecimal(reader.GetOrdinal("Money"));
                activistsList.Add(activist.UserID, activist);
            }
            return activistsList;
        }
        public Activist BuildActivist(SqlDataReader reader)
        {
            Activist activist = new Activist();

            while (reader.Read())
            {
                activist.UserID = reader.GetString(reader.GetOrdinal("UserID"));
                activist.Name = reader.GetString(reader.GetOrdinal("Name"));               
                activist.Address = reader.GetString(reader.GetOrdinal("Address"));
                activist.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                activist.Money = reader.GetDecimal(reader.GetOrdinal("Money"));
            }
            return activist;
        }
        public object ResetList()
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult("select * from Activists", BuildActivistsList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void InsertActivistToDB(string UserID, string Name, string Address, string Phone, decimal Money)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Activists(UserID, Name, Address, Phone, Money) Values('{UserID}','{Name}','{Address}','{Phone}','{Money}')");
            }
            catch (Exception ex)
            {
            }
        }
        public object GetActivistFromDB(string UserID)
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult($"Select * from Activists where UserID= '{UserID}'", BuildActivist);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteActivistFromDB(string UserID)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Activists where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateActivistInDB(string UserID, string Name, string Address, string Phone, decimal Money)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Update Activists set Name='{Name}' , Address='{Address}' , Phone='{Phone}' , Money='{Money}' where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
