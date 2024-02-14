using RealTrading.Shared;

namespace RealTrading.Api.Abstract
{
    public interface IPriceClient
    {
        Task ReceiverPrice(TL tL);
    }
}
