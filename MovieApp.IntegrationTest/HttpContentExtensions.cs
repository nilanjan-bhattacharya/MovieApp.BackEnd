using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.IntegrationTest
{
    /// <summary>Extensions methods for <see cref="HttpContent"/>.</summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Reads the content as a string an deserializes it into an object
        /// of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to read the content as.</typeparam>
        /// <param name="content">The content to read from.</param>
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(dataAsString);
        }
    }
}
