using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using SignalRClient.Models;


namespace SignalRClient.Service
{
    public class SosClient
    {
        HubConnection _connection;
        public IConfiguration Configuration { get; }

        private readonly string _hubUrl;
        private readonly string _sosHandler;
        private readonly string _topicName;

        public SosClient(IConfiguration configuration)
        {
            Configuration = configuration;
            _hubUrl = Configuration.GetValue<String>("SignalRHubURL");
            _sosHandler = Configuration.GetValue<String>("SosHandlerName");
            _topicName = Configuration.GetValue<String>("SosTopicName");
            Console.WriteLine("From Sos Client::: HubURL:"+_hubUrl+" || SOSHandlerName:"+_sosHandler+" || TopicName:"+_topicName);
        }

        public async Task Connect()
        {
            _connection = new HubConnectionBuilder().WithUrl(_hubUrl).WithAutomaticReconnect().Build();
            await _connection.StartAsync();
        }


        public void ReceiveMessagesFromHub(String topic)
        {
            _connection.On<MapPopupMessage>(_topicName, (message) =>
            {
                Debug.WriteLine("||||||||||||||Received message is : "+message.Content);
            });
            _connection.Closed += ConnectionOnClosed;
        }

        private Task ConnectionOnClosed(Exception arg)
        {
            _connection.Remove(_sosHandler);
            Debug.WriteLine("Closed..................");
            return Task.CompletedTask;
        }

        public async Task PushSosToHub(MapPopupMessage emergencyInfo)
        {
            await _connection.SendAsync(_sosHandler, _topicName, emergencyInfo);
        }

    }
}
