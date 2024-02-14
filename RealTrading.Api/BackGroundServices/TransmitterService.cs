using Binance.Spot;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using RealTrading.Api.Abstract;
using RealTrading.Api.Hubs;
using RealTrading.Shared;

namespace RealTrading.Api.BackGroundServices
{
    public class TransmitterService : BackgroundService
    {
        private TL tl = new TL() { Price = "0"};
        private readonly IHubContext<PriceHub, IPriceClient> _hubContext;

        public TransmitterService(IHubContext<PriceHub, IPriceClient> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Console.Out.WriteLineAsync("Background Service başladı");
            var websocket = new MarketDataWebSocket("btcusdt@trade");
            websocket.OnMessageReceived(
                async (data) =>
                {
                    JObject json = JObject.Parse(data);
                    tl = new TL() { Price = (string)json["p"] };
                    await _hubContext.Clients.All.ReceiverPrice(tl);
                }, stoppingToken);

            await websocket.ConnectAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000); // Burada bir bekleme süresi ekleyerek sürekli dönmesini önleyebilirsiniz.
            }
        }

    }
}
