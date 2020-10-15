using System.Threading.Tasks;

namespace APITest.BI
{
    public interface IEmailProvider
    {
        Task SendAsync(string destination, string subject, string body, int retryCount);
    }
}
