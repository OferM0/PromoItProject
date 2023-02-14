using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Entities.Commands
{/*
    public class RolesGetCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param.Length == 0) //get all
            {
                try
                {
                    
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }
            }
            else //get by id
            {
                string id = (string)param[0];
                if (id != null)
                {
                    try
                    {
                    }
                    catch (Exception ex)
                    {
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    return "Error: Missing parameters";
                }
            }

        }
    }

    public class RolesAddCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param.Length > 0)
            {
                string body = (string)param[1];
                if (body != null) //update only by id
                {
                    try
                    {
                       
                    }
                    catch (Exception ex)
                    {
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    return "Error: Missing parameters";
                }
            }
            else
            {
                return "Error: Missing parameters";
            }
        }
    }*/
}
