using System.Threading.Tasks;
using Web.Domain;

namespace Web.Core.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<bool> SomeCustomMethodAsync();
    }
}
