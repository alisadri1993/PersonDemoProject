using PersonDataProcessor.DAL.Repositories;
using System;

namespace PersonDataProcessor.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository PersonRepository { get; }
        void commit();
    }
}
