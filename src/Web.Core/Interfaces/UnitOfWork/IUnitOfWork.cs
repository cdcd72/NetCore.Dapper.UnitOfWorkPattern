using System;

namespace Web.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 客戶
        /// </summary>
        ICustomerRepository Customer { get; }

        /// <summary>
        /// 訂單
        /// </summary>
        IOrderRepository Order { get; }

        /// <summary>
        /// 儲存變更
        /// </summary>
        void SaveChanges();
    }
}
