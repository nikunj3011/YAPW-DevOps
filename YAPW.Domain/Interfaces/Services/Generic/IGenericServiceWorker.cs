using System;
using System.Threading.Tasks;

namespace YAPW.Domain.Interfaces.Services.Generic
{
    public interface IGenericServiceWorker : IDisposable
    {
        Task SaveAsync();
        void Save();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollBackTransaction();
    }
}
