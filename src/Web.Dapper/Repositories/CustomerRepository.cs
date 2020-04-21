using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Web.Core;
using Web.Core.Interfaces;
using Web.Domain;

namespace Web.Dapper.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        #region Constructor

        public CustomerRepository(IDbTransaction transaction) : base(transaction) { }

        #endregion

        /// <summary>
        /// 新增客戶資料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override async Task<int> InsertAsync(Customer entity)
        {
            string sql = @"Insert into Customers 
                            (CustomerID, CompanyName) 
                           Values 
                            (@CustomerID, @CompanyName)";

            return await Conn.ExecuteAsync(sql, entity, Trans);
        }

        /// <summary>
        /// 取得全部客戶資料
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Customer>> GetAllAsync()
        {
            string sql = @"Select * from Customers";

            return await Conn.QueryAsync<Customer>(sql, null, Trans);
        }

        /// <summary>
        /// 依據ID取得客戶資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Customer> FindAsync(object id)
        {
            string sql = @"Select * from Customers where CustomerID = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);

            return await Conn.QueryFirstAsync<Customer>(sql, parameters, Trans);
        }

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        public override async Task<int> UpdateAsync(Customer entityToUpdate)
        {
            string sql = @"Update Customers Set 
                            CompanyName = @CompanyName
                           Where CustomerID = @CustomerID";

            return await Conn.ExecuteAsync(sql, entityToUpdate, Trans);
        }

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(object id)
        {
            string sql = @"Delete Customers where CustomerID = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);

            return await Conn.ExecuteAsync(sql, parameters, Trans);
        }

        /// <summary>
        /// 一些客製化非同步方法
        /// </summary>
        /// <returns></returns>
        public Task<bool> SomeCustomMethodAsync()
        {
            throw new NotImplementedException();
        }
    }
}
