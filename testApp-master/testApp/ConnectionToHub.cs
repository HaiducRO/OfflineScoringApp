using ArcheryEntities.Entities;
using Microsoft.AspNetCore.SignalR.Client;

namespace testApp
{
    public class ConnectionToHub
    {
        private readonly string baseAddress = "https://archerydream.com";
        //private readonly string baseAddress ="https://localhost:5005";
        private HubConnection HubConnection { get; set; }
        public string GetConnectionID()
        {
            return HubConnection.ConnectionId;
        }

        public async Task StartConnection()
        {
            if (HubConnection == null)
            {
                HubConnection = new HubConnectionBuilder()
                    .WithUrl($"{baseAddress}/BasicHub")
                       .WithAutomaticReconnect()
                       .Build();
                //con.On<string>("ReceiveMessage", (message) =>
                //{
                //    _messageRecived = message;
                //    textBox1.Text = _messageRecived;
                //});
                await HubConnection.StartAsync();
            }
        }

        public async Task<FinalScoreBoard?> GetFinalScoreBoardAsync()
        {
            if (HubConnection != null)
            {
                return await HubConnection.InvokeAsync<FinalScoreBoard?>("GetFinalScoreBoardAsync", 2);
            }
            return null;
        }

        public async Task<Target?> GetTargetByIdAsync(int targetId)
        {
            if (HubConnection != null)
            {
                return await HubConnection.InvokeAsync<Target?>("GetTargetByIdAsync", targetId);
            }
            return null;
        }
    }
}
