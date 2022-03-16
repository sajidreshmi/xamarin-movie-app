using System.Threading.Tasks;

namespace Movies
{
    internal interface INetworkService
    {
        Task<TResult> GetTask<TResult>(string uri);
    }
}