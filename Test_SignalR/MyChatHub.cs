using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


namespace Test_SignalR
{
    [HubName("myChatHub")]
    public class MyChatHub : Hub
    {
        public MyChatHub()
        {
            
        }
        /// <summary>
        /// Only one connection data for the entire server. The Hub seems to exist per client.
        /// </summary>
        static ClientConnectionData _clientClientConnectionData = new ClientConnectionData();
        public void send(String message)
        {
            if (Context != null)
            {
                string qq = Context.ConnectionId;

                if (_clientClientConnectionData.TheData.ContainsKey(qq))
                {
                    message = _clientClientConnectionData.TheData[qq].ClientName + ": " + message;
                }
            }
            TheHubContext.Clients.All.addMessage(message);
        }

        IHubContext TheHubContext
        {
            get
            {
                return GlobalHost.ConnectionManager.GetHubContext<MyChatHub>();
            }
        }
        
        public void setName(String newName)
        {
            string qq = Context.ConnectionId;
            String oldName = "Name set to";
            if (_clientClientConnectionData.TheData.ContainsKey(qq))
            {
                oldName = _clientClientConnectionData.TheData[qq].ClientName + " changed names to";
            }
            _clientClientConnectionData.SetClientName(new ClientInfo(newName, Context.ConnectionId));
            TheHubContext.Clients.All.addMessage(oldName+" "+newName);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            _clientClientConnectionData.SetClientName(new ClientInfo("NewClient", Context.ConnectionId));
            //Clients.Client(Context.ConnectionId).send("Hello " + identity.Name);
            TheHubContext.Clients.All.addMessage("New connection received");
            return base.OnConnected();
        }

        public void sendMessageTo(String target, String message)
        {
            var myTarget =
                _clientClientConnectionData.TheData.Where(x => x.Value.ClientName.Equals(target)).Select(x => x.Value);
            if (!myTarget.Any())
            {
                return;
            }
            if (_clientClientConnectionData.TheData.ContainsKey(Context.ConnectionId))
            {
                message = _clientClientConnectionData.TheData[Context.ConnectionId].ClientName + ": " + message;
            }
            foreach (var curTarget in myTarget)
            {

                
                TheHubContext.Clients.Client(curTarget.ClientId).addMessage(message);
            }
        }

       
    }
}