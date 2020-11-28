using System.Threading.Tasks;

namespace DrinkParty.Hubs.Contracts
{
    public interface IGameHubFrontend
    {
        Task NotifyUserJoin(string roomGroup, string userName);
    }
}
