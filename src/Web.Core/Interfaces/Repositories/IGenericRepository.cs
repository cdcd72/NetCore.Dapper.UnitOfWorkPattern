using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Core.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<int> InsertAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> FindAsync(object id);

        Task<int> DeleteAsync(object id);

        Task<int> UpdateAsync(T entityToUpdate);
    }
}
