using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.DataSubSystem.Contract
{
    public interface IJsonRW
    {
        Task<IEnumerable<T>> Read<T>();
        Task<T> Write<T>(T data);
    }
}
