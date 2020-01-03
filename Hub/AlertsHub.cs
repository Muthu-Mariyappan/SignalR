using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRClient.Models;

namespace SignalRClient.Hub
{
    
    public class AlertsHub:Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SosHandler(string topicName, MapPopupMessage message)
        {
            await Clients.All.SendAsync(topicName, message);
            Console.WriteLine("Hub is called with message : "+message.Content["Name"]);
        }
    }
}