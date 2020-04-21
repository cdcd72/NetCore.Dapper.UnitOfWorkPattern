using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Web.Core;
using Web.Core.Interfaces;
using Web.Domain;

namespace Web.Dapper.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        #region Constructor

        public OrderRepository(IDbTransaction transaction) : base(transaction) { }

        #endregion

        /// <summary>
        /// 新增訂單資料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override async Task<int> InsertAsync(Order entity)
        {
            string sql = @"Insert into Orders 
                            (CustomerID, EmployeeID, ShipVia) 
                           Values 
                            (@CustomerID, @EmployeeID, @ShipVia)";

            return await Conn.ExecuteAsync(sql, entity, Trans);
        }

        /// <summary>
        /// 取得訂單全部資料
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            string sql = @"Select * from Orders";

            return await Conn.QueryAsync<Order>(sql, null, Trans);
        }

        /// <summary>
        /// 依據ID取得訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Order> FindAsync(object id)
        {
            string sql = @"Select * from Orders where OrderID = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);

            return await Conn.QueryFirstAsync<Order>(sql, parameters, Trans);
        }

        /// <summary>
        /// 更新訂單資料
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        public override async Task<int> UpdateAsync(Order entityToUpdate)
        {
            string sql = @"Update Orders Set 
                            ShipName = @ShipName
                           Where OrderID = @OrderID";

            return await Conn.ExecuteAsync(sql, entityToUpdate, Trans);
        }

        /// <summary>
        /// 刪除訂單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(object id)
        {
            string sql = @"Delete Orders where OrderID = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);

            return await Conn.ExecuteAsync(sql, parameters, Trans);
        }
    }
}
