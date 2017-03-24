using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_SignalR
{
    public class ClientConnectionData
    {
        private ConcurrentDictionary<string, ClientInfo> _theData = new ConcurrentDictionary<string, ClientInfo>();

        public ConcurrentDictionary<string, ClientInfo> TheData
        {
            get { return _theData; }
        }

        public void SetClientName(ClientInfo clientInfo)
        {
            _theData.AddOrUpdate(clientInfo.ClientId, clientInfo,
                (key, existingVal) =>
                {
                    // If this delegate is invoked, then the key already exists. 
                    // The only updatable fields are the temerature array and lastQueryDate.
                    existingVal.ClientName = clientInfo.ClientName;
                    return existingVal;
                });

        }

        
    }
}