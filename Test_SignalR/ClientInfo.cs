using System;

namespace Test_SignalR
{
    public class ClientInfo
    {
        public ClientInfo(string clientName, String clientID)
        {
            ClientName = clientName;
            ClientId = clientID;
        }

        public String ClientName { get; set; }
        public string ClientId { get; set; }
    }
}