﻿using System;
using System.Data;
using Web.Core.Interfaces;
using Web.Dapper.Repositories;

namespace Web.Dapper.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        
        public ICustomerRepository Customer =>
            _customerRepository ?? (_customerRepository = new CustomerRepository(_transaction));


        public IOrderRepository Order => 
            _orderRepository ?? (_orderRepository = new OrderRepository(_transaction));

        #endregion

        #region Constructor

        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.GetConnection();
            _connection.Open();

            // 開始交易
            _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        #endregion

        /// <summary>
        /// 儲存變更
        /// </summary>
        public void SaveChanges()
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
                _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
                ResetRepositories();
            }
        }

        #region Private Method

        /// <summary>
        /// 重置資源池
        /// </summary>
        private void ResetRepositories()
        {
            _customerRepository = null;
            _orderRepository = null;
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 釋放交易資源
                    _transaction?.Dispose();
                    _transaction = null;

                    // 釋放連線資源
                    _connection?.Dispose();
                    _connection = null;
                }

                disposedValue = true;
            }
        }

        ~UnitOfWork() {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
