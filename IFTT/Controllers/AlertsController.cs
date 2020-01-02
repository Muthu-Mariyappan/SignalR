﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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
            //_sosClient.ReceiveMessagesFromHub("SOSMessages");
            Debug.WriteLine("Established");
        }

        [HttpPost("sos")]
        public async Task SendSosAlert([FromBody] String emergencyInfo)
        {
            await _sosClient.PushSosToHub(emergencyInfo);
        }
    }
}