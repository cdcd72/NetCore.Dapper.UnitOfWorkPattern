using System.Threading.Tasks;
using Web.Core;
using Web.Domain;

namespace Web.Repositories.Interface
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<bool> SomeCustomMethodAsync();
    }
}
