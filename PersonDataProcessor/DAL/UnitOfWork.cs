using Microsoft.Extensions.Options;
using PersonDataProcessor.DAL.Repositories;
using PersonDataProcessor.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PersonDataProcessor.DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IPersonRepository _personRepository;
        private bool _disposed;
        private readonly Setting setting;

        public UnitOfWork(IOptions<Setting> setting)
        {
            this.setting = setting.Value;
            _connection = new SqlConnection(setting.Value.SqlConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IPersonRepository PersonRepository
        {
            get { return _personRepository ?? (_personRepository = new PersonRepository(_transaction)); }
        }

        public void commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _personRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }
    }
}
