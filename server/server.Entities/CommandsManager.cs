using server.Entities.Commands;
using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities;

namespace server.Entities
{
    public class BaseCommand
    {

    }

    public interface ICommand
    {
        object Execute(params object[] param);
    }

    public class CommandsManager
    {
        private Dictionary<string, ICommand> _CommandList;
        public Dictionary<string, ICommand> CommandList
        {
            get
            {
                if(_CommandList == null) Init();
                return _CommandList;
            }
        }
        private void Init()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initializing _CommandList in CommandManager." });

                //build all dictionary
                _CommandList = new Dictionary<string, ICommand>();

                //Campaign Commands
                _CommandList.Add("Campaigns.Get", new CampaignsGetCmd());
                _CommandList.Add("Campaigns.Add", new CampaignsAddCmd());
                _CommandList.Add("Campaigns.Remove", new CampaignsRemoveCmd());
                _CommandList.Add("Campaigns.Update", new CampaignsUpdateCmd());

                //Contacts Commands
                _CommandList.Add("Contacts.Get", new ContactsGetCmd());
                _CommandList.Add("Contacts.Add", new ContactsAddCmd());
                _CommandList.Add("Contacts.Remove", new ContactsRemoveCmd());
                _CommandList.Add("Contacts.Update", new ContactsUpdateCmd());

                //Products Commands
                _CommandList.Add("Products.Get", new ProductsGetCmd());
                _CommandList.Add("Products.Add", new ProductsAddCmd());
                _CommandList.Add("Products.Remove", new ProductsRemoveCmd());
                _CommandList.Add("Products.Update", new ProductsUpdateCmd());

                //Users Commands
                _CommandList.Add("Users.Get", new UsersGetCmd());
                _CommandList.Add("Users.Add", new UsersAddCmd());
                _CommandList.Add("Users.Remove", new UsersRemoveCmd());
                _CommandList.Add("Users.Update", new UsersUpdateCmd());
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to initialize _CommandList in CommandManager." });
                throw;
            }
        }
    }
}
