using System.Threading.Tasks;

namespace Movies
{
    public interface INetworkService
    {
        Task<TResult> GetTask<TResult>(string uri);
    }
}