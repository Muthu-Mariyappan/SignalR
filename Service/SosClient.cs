using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;


namespace SignalRClient.Service
{
    public class SosClient
    {
        HubConnection _connection;
        public IConfiguration Configuration { get; }

        private readonly string _sosHandler;
        private readonly string _hubUrl;

        public SosClient(IConfiguration configuration)
        {
            Configuration = configuration;
            _hubUrl = Configuration.GetValue<String>("SignalRHubURL");
            _sosHandler = Configuration.GetValue<String>("SosHandlerName");
            Debug.WriteLine(_hubUrl);
        }

        public async Task Connect()
        {
            _connection = new HubConnectionBuilder().WithUrl(_hubUrl).WithAutomaticReconnect().Build();
            await _connection.StartAsync();
        }


        public void ReceiveMessagesFromHub(String topic)
        {
            _connection.On<string>(topic, (message) =>
            {
                Debug.WriteLine(message);
            });
            _connection.Closed += ConnectionOnClosed;
        }

        private Task ConnectionOnClosed(Exception arg)
        {
            _connection.Remove(_sosHandler);
            Debug.WriteLine("Closed..................");
            return Task.CompletedTask;
        }

        public async Task PushSosToHub(String emergencyInfo)
        {
            await _connection.SendAsync(_sosHandler, emergencyInfo);
        }

    }
}
