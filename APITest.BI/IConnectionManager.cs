using System.Data;

namespace APITest.BI
{
    public interface IConnectionManager
    {
        IDbConnection DefaultConnection();
    }
}
