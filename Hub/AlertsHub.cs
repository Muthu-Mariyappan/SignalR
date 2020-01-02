﻿using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRClient.Hub
{
    
    public class AlertsHub:Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SosHandler(string message)
        {
            await Clients.All.SendAsync("SOSMessages", message);
            Debug.WriteLine("Hub is called with message : "+message);
        }
    }
}