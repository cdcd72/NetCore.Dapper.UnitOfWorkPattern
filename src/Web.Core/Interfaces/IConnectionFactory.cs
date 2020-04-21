using System.Data;

namespace Web.Core.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
