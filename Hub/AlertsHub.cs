using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRClient.Hub
{
    
    public class AlertsHub:Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SosHandler(string topicName,string message)
        {
            await Clients.All.SendAsync(topicName, message);
            Console.WriteLine("Hub is called with message : "+message);
        }
    }
}