using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SignalRClient.Models;
using SignalRClient.Service;

namespace SignalRClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly SosClient _sosClient;

        public  AlertsController(IConfiguration configuration)
        { 
            _sosClient = new SosClient(configuration);
            _sosClient.Connect().Wait();
            _sosClient.ReceiveMessagesFromHub("SOSMessages");
            Console.WriteLine("Established");
        }

        [HttpGet]
        public string dummy() => "ok its working";
        
        [HttpPost("sos")]
        public async Task SendSosAlert(MapPopupMessage emergencyInfo)
        {
            await _sosClient.PushSosToHub(emergencyInfo);
        }
    }
}
