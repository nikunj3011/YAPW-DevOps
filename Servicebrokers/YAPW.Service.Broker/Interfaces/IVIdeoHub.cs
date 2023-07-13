using System.Threading.Tasks;

namespace YAPW.Service.Broker.Interfaces
{
    public interface IVIdeoHub
    {
        Task ReceiveMessage(string user, string message);
        Task Notify(string message);
    }
}
