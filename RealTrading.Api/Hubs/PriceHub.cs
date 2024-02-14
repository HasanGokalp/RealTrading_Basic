using Microsoft.AspNetCore.SignalR;
using RealTrading.Api.Abstract;

namespace RealTrading.Api.Hubs
{
    public class PriceHub : Hub<IPriceClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).ReceiverPrice(new Shared.TL() { Price = "0"});

            await base.OnConnectedAsync();
        }
    }
}
