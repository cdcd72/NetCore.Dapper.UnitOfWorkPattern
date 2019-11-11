using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Web.Core
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Properties

        protected IDbTransaction Trans { get; }

        protected IDbConnection Conn => Trans.Connection;

        #endregion

        #region Constructor

        protected GenericRepository(IDbTransaction transaction)
        {
            Trans = transaction;
        }

        #endregion

        public abstract Task<int> InsertAsync(T entity);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T> FindAsync(object id);

        public abstract Task<int> UpdateAsync(T entityToUpdate);

        public abstract Task<int> DeleteAsync(object id);
    }
}
